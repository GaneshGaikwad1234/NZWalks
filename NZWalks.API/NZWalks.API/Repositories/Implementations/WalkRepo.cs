using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementations
{
    public class WalkRepo : IWalkRepo
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepo(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<bool> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            var result=await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            if(result==null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteWalkAsync(Guid id)
        {
            var result=await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(result==null)
            {
                return false;
            }
            var value=nZWalksDbContext.Walks.Remove(result);
            await nZWalksDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await 
                nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk=
                await nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(y=>y.Id==id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk=await nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(existingWalk==null)
            {
                return null;
            }
            existingWalk.Name=walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId=walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
