using dev_habit.Data;
using dev_habit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace dev_habit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DevHabitDbContext _dbContext;

        public RegionsController(DevHabitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = _dbContext.Regions.ToList();
            var regionDTO = new List<RegionDTO>();
            foreach (var item in regionsDomain)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    RegionImgURL = item.RegionImgURL
                });
            }

            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgURL = region.RegionImgURL
            };
            return Ok(regionDTO);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var regionDomain = new Region
            {
                Code = createRegionDTO.Code,
                Name = createRegionDTO.Name,
                RegionImgURL = createRegionDTO.RegionImgURL
            };

            _dbContext.Regions.Add(regionDomain);
            _dbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImgURL = regionDomain.RegionImgURL
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var regionDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            regionDomain.Code = updateRegionDTO.Code;
            regionDomain.Name = updateRegionDTO.Name;
            regionDomain.RegionImgURL = updateRegionDTO.RegionImgURL;

            _dbContext.SaveChanges();
            var updateRegion = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImgURL = regionDomain.RegionImgURL
            };
            return Ok(updateRegion);
        }



    }
}