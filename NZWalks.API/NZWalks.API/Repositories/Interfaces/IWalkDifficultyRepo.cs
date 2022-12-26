using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkDifficultyRepo
    {
        Task<List<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty>AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
        Task<bool>DeleteWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty>UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty);
    }
}
