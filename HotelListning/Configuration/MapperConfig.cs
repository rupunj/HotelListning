using System;
using AutoMapper;
using HotelListning.Models;
using HotelListning.Data;
namespace HotelListning.Configuration
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<CreateCountryDto, Country>().ReverseMap();
			CreateMap<Country, GetCountryDto>();
			CreateMap<GetCoutryDetailsDto, Country>().ReverseMap();
			CreateMap<Hotel, GetHotelDto>().ReverseMap();
			CreateMap<UpdateCoutryDto, Country>();
		}
	}
}

