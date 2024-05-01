using GeoSpotter.API.Persistence.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace GeoSpotter.API.Authorization;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string BasicAuthenticationKey = "basic";

    private readonly IUserRepository _userRepository;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IUserRepository userRepository)
        : base(options, logger, encoder)
    {
        _userRepository = userRepository;
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers.Authorization.ToString();

        if (!IsAuthorizationHeaderValid(authorizationHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var token = authorizationHeader[BasicAuthenticationKey.Length..];
        var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var credentials = credentialAsString.Split(":");

        if (credentials.Length != 2)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var userName = credentials[0];
        var password = credentials[1];
        var userResult = await _userRepository.TryGetUserByUsernameAndPassword(userName, password);

        if (userResult.IsFailed)
        {
            return AuthenticateResult.Fail($"Unauthorized: {string.Join(", ", userResult.Errors)}");
        }

        var user = userResult.Value;
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var identity = new ClaimsIdentity(claims, BasicAuthenticationKey);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }

    private static bool IsAuthorizationHeaderValid(string authorizationHeader)
        => !string.IsNullOrEmpty(authorizationHeader)
            && authorizationHeader.StartsWith(BasicAuthenticationKey, StringComparison.OrdinalIgnoreCase);
}
