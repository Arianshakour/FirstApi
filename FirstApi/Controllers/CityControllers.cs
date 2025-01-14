using AutoMapper;
using Azure;
using FirstApi.Entities;
using FirstApi.Models;
using FirstApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/city")]
    public class CityControllers : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityControllers(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cityha = await _cityRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityDto>>(cityha));
            //var cityha = await _cityRepository.GetCitiesAsync();
            //var result = new List<CityDto>();
            //foreach (var city in cityha)
            //{
            //    result.Add(new CityDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Des = city.Des
            //    });
            //}
            //return Ok(result);

            ////var cityha = await _cityRepository.GetCitiesAsync();
            ////var result = cityha.Select(city => new CityDto
            ////{
            ////    Id = city.Id,
            ////    Name = city.Name,
            ////    Des = city.Des
            ////}).ToList();
            ////return Ok(result);
            //////var result = _context.Cities;
            //////return Ok(result);

        }
        [HttpGet("{id}" , Name = "GetCity")]
        public async Task<ActionResult<CityDto>> GetCity(int id, bool includePoints=true)
        {
            var city = await _cityRepository.GetCityAsync(id, includePoints);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CityDto>(city));

            //var city = await _cityRepository.GetCityAsync(id, includePoints);
            //var result = new CityDto()
            //{
            //    Id = city.Id,
            //    Name = city.Name,
            //    Des = city.Des,
            //    POIs = city.POIs.Select(p => new PointOfInterestDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Des
            //    }).ToList()
            //};
            //return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CityDto>> Create([FromBody] CityForCreateDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var c = new City(city.Name)
            {
                Name = city.Name,
                Des = city.Des
            };
            await _cityRepository.CreateAsync(c);
            await _cityRepository.SaveAsync();
            var created = new CityDto()
            {
                Id = c.Id,
                Name = c.Name,
                Des = c.Des
            };
            return CreatedAtAction("GetCity", new
            {
                id = created.Id,
                includePoints = false
            }, created);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, CityForEditDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var c = await _cityRepository.GetCityAsync(id, false);
            if (c== null)
            {
                return NotFound();
            }
            c.Name = city.Name;
            c.Des = city.Des;
            _cityRepository.Edit(c);
            await _cityRepository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> EditPartial(int id, JsonPatchDocument<CityForEditDto> json)
        {
            var c = await _cityRepository.GetCityAsync(id, false);
            if (c == null)
            {
                return NotFound();
            }
            var cForPatch = new CityForEditDto()
            {
                Name = c.Name,
                Des = c.Des
            };
            json.ApplyTo(cForPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(cForPatch))
            {
                return BadRequest();
            }
            c.Name = cForPatch.Name;
            c.Des = cForPatch.Des;
            await _cityRepository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var city = await _cityRepository.GetCityAsync(id, false);
            if (city == null)
            {
                return NotFound();
            }
            _cityRepository.Delete(city);
            await _cityRepository.SaveAsync();
            return NoContent();
        }
    }
}
