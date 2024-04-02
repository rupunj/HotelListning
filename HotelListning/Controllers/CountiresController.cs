
using HotelListning.Data;
using Microsoft.AspNetCore.Mvc;
using HotelListning.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelListning.Contracts;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountiresController : ControllerBase
    {

        //public readonly HotelListningDbContext _context;
        public readonly IMapper _mapper;

        public readonly ICountriesRepository _repository;

        //public CountiresController(HotelListningDbContext context,IMapper mapper )
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        public CountiresController(ICountriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //[HttpGet("{Id}")]
        //public ActionResult<GetCoutryDetailsDto> GetCountry(int Id)
        //{
        //    var country = _context.Countries.Include(p => p.Hotels).FirstOrDefault(q => q.Id == Id);

        //    if (country is null)
        //    {
        //        return NotFound();
        //    }

        //    var coutryDto = _mapper.Map<GetCoutryDetailsDto>(country);

        //    return Ok(coutryDto);
        //}

        [HttpGet("{Id}")]
        public async Task<ActionResult<GetCoutryDetailsDto>> GetCountry(int Id)
        {
           var details =  await _repository.GetDetails(Id);
           var returnDto = _mapper.Map<GetCoutryDetailsDto>(details);

            return Ok(returnDto);
        }
        [HttpGet]
        public async Task< ActionResult<ICollection<GetCountryDto>>> GetCountries()
        {

            //var countires = _context.Countries.ToList();

            var countries = await _repository.GetAllAsync();
            var record = _mapper.Map<List<GetCountryDto>>(countries);

            return Ok(record);
        }

        [HttpPost]
        public async Task< ActionResult<ICollection<Country>>> CreateCountry(CreateCountryDto countryDto)
        {



            var country = _mapper.Map<Country>(countryDto);

           await  _repository.AddAsync(country);
            //_context.Countries.Add(country);
            //_context.SaveChanges();

            return CreatedAtAction("GetCountries", country);

        }
        [HttpDelete("{Id}")]
        public ActionResult DeleteHotel(int Id)
        {
            //var country = _context.Countries.Find(Id);
            var country = _repository.GetAsync(Id);

            if (country is null)
            {
                return NotFound();
            }
            //_context.Remove(country);
            //_context.SaveChanges();

            _repository.DeleteAsync(Id);

            return NoContent();
        }
        [HttpPut("{Id}")]
        public async Task <ActionResult> UpdateCoutry(int Id, UpdateCoutryDto updateCoutry)
        {
            if (Id != updateCoutry.Id)
            {
                return BadRequest("Invalid Request");
            }

            //var coutry = _context.Countries.Find(Id);
            var coutry =  await _repository.GetAsync(Id);

            //var country = _mapper.Map<Country>(countrytask);

            _mapper.Map(updateCoutry, coutry);

            await _repository.UpdateAsync(coutry);

          
          //  _context.SaveChanges();

            return CreatedAtAction("GetCountry", new { Id = Id }, updateCoutry);
        }

    }
}

