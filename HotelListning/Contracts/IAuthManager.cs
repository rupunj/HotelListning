using System;
using HotelListning.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListning.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDto userDto);
		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<string> CreateRefreshToken();
		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);


    }
}

