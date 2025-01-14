using AutoMapper;
using FirstApi;
using FirstApi.Entities;
using FirstApi.Models;
using FirstApi.Repositories.Interfaces;
using FirstApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FirstApi.Controllers
{
    [Route("api/city/{cityid}/pointofinterest")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mail;
        private readonly CityDataStore _context;
        private readonly IMapper _mapper;
        private readonly IPointRepository _pointRepository;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mail, CityDataStore context
            , IMapper mapper, IPointRepository pointRepository)
        {
            _logger = logger;
            _mail = mail;
            _context = context;
            _mapper = mapper;
            _pointRepository = pointRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterests(int cityid)
        {
            if (!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            var city = await _pointRepository.GetPointsAsync(cityid);

            var points = new List<PointOfInterestDto>();
            foreach (var point in city)
            {
                points.Add(new PointOfInterestDto
                {
                    Id = point.Id,
                    Name = point.Name,
                    Description = point.Des
                });
            }
            //return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(city));
            return Ok(points);
            //var points = city.Select(x=>new PointOfInterestDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Des
            //}).ToList();

            ////try
            ////{
            ////    throw new Exception("exception sample");
            ////    var city = _context.Cities.FirstOrDefault(x => x.Id == cityid);
            ////    if (city == null)
            ////    {
            ////        return NotFound();
            ////    }
            ////    return Ok(city.POIs);
            ////}
            ////catch (Exception ex)
            ////{
            ////    _logger.LogCritical($"exception getting {cityid}", ex);
            ////    return StatusCode(500, "a problem happened");
            ////}
        }
        [HttpGet("{pointofinterestid}" , Name = "GetPointOfInterest")] //name dar action badi estefade shode
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest (int cityid , int pointofinterestid)
        {
            if(!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            var p = await _pointRepository.GetPointAsync(cityid, pointofinterestid);
            if (p == null)
            {
                return NotFound();
            }
            var result = new PointOfInterestDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Des
            };
            return Ok(result);
            //return Ok(_mapper.Map<PointOfInterestDto>(p));

            //var city = _context.Cities.FirstOrDefault(x => x.Id == cityid);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var result = city.POIs.FirstOrDefault(x=>x.Id== pointofinterestid);
            //if(result== null)
            //{
            //    return NotFound();
            //}
            //return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> Create(int cityid , [FromBody]PointOfInterestForCreateDto point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            //var result = _mapper.Map<PointOfInterest>(point);
            var result = new PointOfInterest(point.Name)
            {
                CityId = cityid,
                Des = point.Description
            };
            await _pointRepository.CreateAsync(cityid, result);
            await _pointRepository.SaveAsync();
            //hala baraye namayesh oni ke ijad kardim dobare tabdil be dto mikonim albate dto aadi na createdto
            //var created = _mapper.Map<PointOfInterestDto>(result);
            var created = new PointOfInterestDto()
            {
                Id=result.Id,
                Name = result.Name,
                Description=result.Des
            };
            return CreatedAtAction("GetPointOfInterest", new
            {
                cityid = cityid,
                pointofinterestid = created.Id
            }, created);
            //var city = _context.Cities.FirstOrDefault(x => x.Id == cityid);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var MaxPOIId = _context.Cities.SelectMany(x => x.POIs).Max(x => x.Id);
            //var create = new PointOfInterestDto()
            //{
            //    Id = ++MaxPOIId,
            //    Name = point.Name,
            //    Description = point.Description,
            //};
            //city.POIs.Add(create);
            ////chon bayad status 201 create bargardone bja OK az Created estefade shod
            ////chon mikhaim address oni ke zakhire shode bargardone CreatedAtAction zadim ta kamel tar bashe
            //return CreatedAtAction("GetPointOfInterest", new
            //{
            //    cityid = cityid,
            //    pointofinterestid = create.Id
            //},
            //create
            //);
            ////goftam boro be action GetPointOfInterest ke hamoon action balaei hast
            ////chon action balaei 2 ta vorodi dare behesh dadam meqdarasho
            ////chon mikhaim shei ke ijad kardim bargardoonim to return create ham dadim 
        }
        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> Edit (int cityid, int pointofinterestid, PointOfInterestForEditDto point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            var p = await _pointRepository.GetPointAsync(cityid, pointofinterestid);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = point.Name;
            p.Des = point.Description;
            //var r = _mapper.Map(point , p);//inja injoori benevis
            _pointRepository.Edit(p);
            await _pointRepository.SaveAsync();
            return NoContent();
            //var city = _context.Cities.FirstOrDefault(x => x.Id == cityid);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //var p = city.POIs.FirstOrDefault(x => x.Id == pointofinterestid);
            //if (p == null)
            //{
            //    return NotFound();
            //}
            //p.Name = point.Name;
            //p.Description = point.Description;
        }
        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartialEdit(int cityid,int pointofinterestid, 
            JsonPatchDocument<PointOfInterestForEditDto> json)
        {
            //inja 2ta package download kardim 1-newton 2-jsonpatch va bala estefade kardim
            if(!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            var p = await _pointRepository.GetPointAsync (cityid, pointofinterestid);
            if(p == null)
            {
                return NotFound();
            }
            //chon be sorat donei mikhaim update konim bayad ye vaset dar nazar bgirim
            var pForPatch = new PointOfInterestForEditDto()
            {
                Name = p.Name,
                Description = p.Des
            };
            ////var pForPatch = _mapper.Map<PointOfInterestForEditDto>(p);
            json.ApplyTo(pForPatch, ModelState);
            //to patch model state injoori baresi mishe ke albate age model valid nabood hich kari nemikone
            //pas ma khat paein ro zadim ta be karbar neshon bede
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //in validation bala roye model aslimoon hast ye validation mikham vaqti to postman
            //dare mizane yeho remove nakone field ke ejbari hast mese name ra
            if (!TryValidateModel(pForPatch))
            {
                return BadRequest();
            }
            p.Name = pForPatch.Name;
            p.Des = pForPatch.Description;
            //_mapper.Map(pForPatch,p);//chon to chizi narikhtim mese adam neveshtim yani avalio be dovomi tabdil kon
            //havaset be profile bashe bayad joftesh bashe ham 1be2 ham 2be1
            await _pointRepository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> Delete(int cityid , int pointofinterestid)
        {
            if (!await _pointRepository.CityExistAsync(cityid))
            {
                return NotFound();
            }
            var p = await _pointRepository.GetPointAsync ( cityid, pointofinterestid);
            if (p == null)
            {
                return NotFound();
            }
            _pointRepository.Delete(p);
            await _pointRepository.SaveAsync();
            _mail.Send("Point deleted", $"{p.Name} with id {p.Id}");
            return NoContent();
        }
    }
}