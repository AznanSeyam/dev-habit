using dev_habit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var regions = _dbContext.Regions.ToList();
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionid = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionid == null)
            {
                return NotFound();
            }

            else
                return Ok(regionid);
        }

    }
}