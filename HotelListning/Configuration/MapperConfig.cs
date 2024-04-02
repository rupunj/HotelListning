using System;
using AutoMapper;
using HotelListning.Models;
using HotelListning.Data;
using HotelListning.Models.Hotel;
using HotelListning.Models.Country;
using HotelListning.Models.User;

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
			CreateMap<CreateHotelDto, Hotel>();
			CreateMap<UpdateHotelDto, Hotel>();
			CreateMap<Hotel, GetHotelCountryDto>();
			CreateMap<Country, GetCountryDetailsDto>();

			CreateMap<ApiUserDto, ApiUser>().ReverseMap();


		}
	}
}

