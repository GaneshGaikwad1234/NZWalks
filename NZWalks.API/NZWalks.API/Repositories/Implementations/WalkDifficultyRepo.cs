using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementations
{
    public class WalkDifficultyRepo : IWalkDifficultyRepo
    {
        private readonly NZWalksDbContext nZWalkDbContext;

        public WalkDifficultyRepo(NZWalksDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
           walkDifficulty.Id=Guid.NewGuid();
           await nZWalkDbContext.WalkDifficulty.AddAsync(walkDifficulty);
           await nZWalkDbContext.SaveChangesAsync();
           return walkDifficulty;   

        }

        public async Task<bool> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty=await nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(x=>x.Id==id);
            if(walkDifficulty==null)
            {
                return false;
            }
            nZWalkDbContext.WalkDifficulty.Remove(walkDifficulty);
            await nZWalkDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalkDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty=await nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(walkDifficulty==null)
            {
                return null;
            }
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(y => y.Id == id);
            if(existingWalkDifficulty==null)
            {
                return null;
            }
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalkDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
