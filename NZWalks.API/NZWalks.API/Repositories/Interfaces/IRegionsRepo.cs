using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionsRepo
    {
        Task<List<Region>> GetAllAsync();
    }
}
