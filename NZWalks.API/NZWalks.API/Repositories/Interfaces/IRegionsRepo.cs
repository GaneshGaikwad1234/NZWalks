using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionsRepo
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
        Task<bool>DeleteAsync(Guid id);
        Task<Region>UpdateAsync(Guid id, Region region);
    }
}
