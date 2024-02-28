using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListning.Contracts;
using HotelListning.Data;
using HotelListning.Models.Hotel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        public readonly IHotelsRepository _hotelsRepository;
        public readonly IMapper _mapper;
        public HotelsController(IHotelsRepository hotelsRepository,IMapper mapper)
        {
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotelsDetails()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            var hoteldto = _mapper.Map<IEnumerable<GetHotelDto>>(hotels);
            return Ok(hoteldto);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<GetHotelCountryDto>> GetHotelDetail(int Id)
        {
            var hotel = await _hotelsRepository.GetHotelFullDetails(Id);

            //var hotel = await _hotelsRepository.GetAsync(Id);

            var hotelDto = _mapper.Map<GetHotelCountryDto>(hotel);

            return Ok(hotelDto);
        }
        [HttpPost]
        public async Task<ActionResult<GetHotelDto>> CreateHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            var returnHotlDto  =   _mapper.Map<GetHotelDto> ( await _hotelsRepository.AddAsync(hotel));

            return CreatedAtAction("GetHotelDetail", new { id = hotel.Id }, returnHotlDto);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateHotel(int Id, UpdateHotelDto updateHotelDto)
        {
            if (Id != updateHotelDto.Id)
            {
                return BadRequest("Id missmatchd");
            }

            var hotel = await _hotelsRepository.GetAsync(Id);

            _mapper.Map(updateHotelDto, hotel);

           await  _hotelsRepository.UpdateAsync(hotel);

            return CreatedAtAction("GetHotelDetail", new { Id = Id }, hotel);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteHotel(int Id)
        {
            if (await _hotelsRepository.GetAsync(Id) is null)
            {
                return NotFound();
            }
            await _hotelsRepository.DeleteAsync(Id);
            return NoContent();
        }
    }
}

