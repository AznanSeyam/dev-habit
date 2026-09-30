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
    }
}