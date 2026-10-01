using dev_habit.Data;
using dev_habit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _dbContext.Regions.ToListAsync();
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var regionDomain = new Region
            {
                Code = createRegionDTO.Code,
                Name = createRegionDTO.Name,
                RegionImgURL = createRegionDTO.RegionImgURL
            };

            await _dbContext.Regions.AddAsync(regionDomain);
            await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            regionDomain.Code = updateRegionDTO.Code;
            regionDomain.Name = updateRegionDTO.Name;
            regionDomain.RegionImgURL = updateRegionDTO.RegionImgURL;

            await _dbContext.SaveChangesAsync();
            var updateRegion = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImgURL = regionDomain.RegionImgURL
            };
            return Ok(updateRegion);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteDomain == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(deleteDomain);
            await _dbContext.SaveChangesAsync();
            var regionDto = new RegionDTO
            {
                Id = deleteDomain.Id,
                Code = deleteDomain.Code,
                Name = deleteDomain.Name,
                RegionImgURL = deleteDomain.RegionImgURL
            };
            return Ok(regionDto);

        }
    }
}