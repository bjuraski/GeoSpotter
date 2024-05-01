using FluentResults;
using GeoSpotter.API.Entities;

namespace GeoSpotter.API.Persistence.Interfaces;

public interface IUserRepository
{
    Task<Result<User>> TryGetUserByUsernameAndPassword(string userName, string password);
}
