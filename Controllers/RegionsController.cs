using AutoMapper;
using dev_habit.Data;
using dev_habit.Models;
using dev_habit.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace dev_habit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();
            return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(region));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            if (ModelState.IsValid)
            {
                var regionDomain = _mapper.Map<Region>(createRegionDTO);

                regionDomain = await _regionRepository.CreateAsync(regionDomain);

                var regionDTO = _mapper.Map<RegionDTO>(regionDomain);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {

            var regionModel = _mapper.Map<Region>(updateRegionDTO);

            var regionDomain = await _regionRepository.UpdateAsync(id, regionModel);
            if (regionModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(regionDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(regionDomain));
        }
    }
}