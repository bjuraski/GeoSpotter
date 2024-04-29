using GeoSpotter.API.Entities;

namespace GeoSpotter.API.Persistence.Interfaces;

public interface IApiMessageRepository
{
    Task AddApiMessageAsync(ApiMessage apiMessage);
}
