using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepo walkDifficultyRepo;

        public WalkDifficultyController(IWalkDifficultyRepo walkDifficultyRepo)
        {
            this.walkDifficultyRepo = walkDifficultyRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            var walkDifficulties=await walkDifficultyRepo.GetAllAsync();
            return Ok(walkDifficulties);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult>GetWalkDifficultyByIdAsync(Guid id)
        {
            var walkDifficulty= await walkDifficultyRepo.GetWalkDifficultyAsync(id);
            if(walkDifficulty==null)
            {
                return NotFound();
            }

            return Ok(walkDifficulty);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest walkDifficultyRequest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficultyRequest.Code
            };
            var walkDifficulty1=await walkDifficultyRepo.AddWalkDifficultyAsync(walkDifficulty);
            return Ok(walkDifficulty1);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var result=await walkDifficultyRepo.DeleteWalkDifficultyAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("WalkDifficulty Deleted Successfully.");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute]Guid id,[FromBody]AddWalkDifficultyRequest updated)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updated.Code
            };
            var Update=await walkDifficultyRepo.UpdateWalkDifficulty(id, walkDifficulty);
            if(Update==null)
            {
                return null; 
            }
            return Ok(Update);
        }
    }
}
