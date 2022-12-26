using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Implementations;
using NZWalks.API.Repositories.Interfaces;
using Region = NZWalks.API.Models.Domain.Region;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsRepo regionsRepo;
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepo regionsRepo,IMapper mapper)
        {
            this.regionsRepo = regionsRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public  async Task<IActionResult> GetAllRegions()
        {
            var regions= await regionsRepo.GetAllAsync();

            //var RegionsDTOList=new List<Models.DTO.Region>();
            //foreach(var region in regions)
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id=region.Id,
            //        Name=region.Name,
            //        Code=region.Code,
            //        Lat=region.Lat,
            //        Long=region.Long,
            //        Population=region.Population,
            //        Area=region.Area,
            //    };
            //    RegionsDTOList.Add(regionDTO);
            //}
            var RegionsDTOList=mapper.Map < List<Models.DTO.Region>>(regions);

            return Ok(RegionsDTOList);
            
            
         }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync( Guid id)
        {
            var region=await regionsRepo.GetAsync(id);
            if(region==null)
            {
                return NotFound("Region is not available.");
            }
            var regionsDTO=mapper.Map<Models.DTO.Region>(region);
            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Models.DTO.AddRegionRequest addregionrequest)
        {
            //Request(DTO) to Domain Model
            var region = new Region()
            {
                Name = addregionrequest.Name,
                Code = addregionrequest.Code,
                Lat = addregionrequest.Lat,
                Long = addregionrequest.Long,
                Population = addregionrequest.Population,
                Area=addregionrequest.Area
            };

            //Pass to repository
             region=await regionsRepo.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id=region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Area = region.Area
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id=regionDTO.Id},regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result=await regionsRepo.DeleteAsync(id);
            if (result == false)
                return NotFound("Region Not Deleted Successfully.");
            else
                return Ok("Region Deleted Successfully.");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest update) 
        {
            //convert DTO to domain model
            var region = new Region
            {
                Name = update.Name,
                Code = update.Code,
                Lat = update.Lat,
                Long = update.Long,
                Area = update.Area,
                Population = update.Population,

            };

            //update region using repository

           region= await regionsRepo.UpdateAsync(id,region);

            //If Null then not found

            if(region==null)
            {
                return NotFound();
            }

            //convert back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Area = region.Area
            };
            //retun Ok response

            return Ok(regionDTO);
        }
}
}
