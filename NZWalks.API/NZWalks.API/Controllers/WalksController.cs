using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;

        public WalksController(IWalkRepo walkRepo,IMapper mapper)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walk=await walkRepo.GetAllWalksAsync();

            var walksDTOList=mapper.Map<List<Models.DTO.Walk>>(walk);
            return Ok(walksDTOList);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            var walk=await walkRepo.GetWalkByIdAsync(id);

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            if(walkDTO==null)
            {
                return NotFound();
            }
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            var result=await walkRepo.AddWalkAsync(walk);
            if(result==false)
            {
                return BadRequest("walk not Added");
            }
            return Ok("Walk Added Successfully.");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var result=await walkRepo.DeleteWalkAsync(id);
            if(result==false)
            {
                return NotFound();
            }
            return Ok("Walk Deleted Successfully.");

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            var result = await walkRepo.UpdateWalkAsync(id, walk);
            if (result == null)
            {
                return NotFound();
            }
            //var walkDTO = new Models.DTO.Walk()
            //{
            //    Id=result.Id,
            //    Name = result.Name,
            //    Length = result.Length,
            //    RegionId = result.RegionId,
            //    WalkDifficultyId = result.WalkDifficultyId,


            //};
            var walkDTO=mapper.Map<Models.DTO.Walk>(result);
            return Ok(walkDTO);
        }
    }
}
