using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

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
            var RegionsDTOList=mapper.Map < List<Models.Domain.Region>>(regions);

            return Ok(RegionsDTOList);
            
            
         }
}
}
