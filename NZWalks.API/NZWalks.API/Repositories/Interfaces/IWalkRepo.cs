using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepo
    {
         Task<List<Walk>> GetAllWalksAsync();
         Task<Walk> GetWalkByIdAsync(Guid id);
         Task <bool> AddWalkAsync(Walk walk);
         Task<bool> DeleteWalkAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);
    }
}
