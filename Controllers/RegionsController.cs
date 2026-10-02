using dev_habit.Data;
using dev_habit.Models;
using dev_habit.Repositories;
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
        private readonly IRegionRepository _regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();
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
            var region = await _regionRepository.GetByIdAsync(id);
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

            regionDomain = await _regionRepository.CreateAsync(regionDomain);

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
            var regionModel = new Region
            {
                Code = updateRegionDTO.Code,
                Name = updateRegionDTO.Name,
                RegionImgURL = updateRegionDTO.RegionImgURL
            };

            var regionDomain = await _regionRepository.UpdateAsync(id, regionModel);
            if (regionModel == null)
            {
                return NotFound();
            }
            var regionDTO = new RegionDTO
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImgURL = regionModel.RegionImgURL
            };
            return Ok(regionDTO);
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
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImgURL = regionDomain.RegionImgURL
            };
            return Ok(regionDto);
        }
    }
}