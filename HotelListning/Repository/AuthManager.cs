﻿using System;
using HotelListning.Contracts;
using HotelListning.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using HotelListning.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace HotelListning.Repository
{
	public class AuthManager :IAuthManager
	{
		private readonly IMapper _mapper;
		private readonly UserManager<ApiUser> _userManager;
		private readonly IConfiguration _configuration;
		private ApiUser _user;

		private const string _loginProvider = "HotelListningAPI";
		private const string _refreshToken = "RefreshToken";


        public AuthManager(IMapper mapper,UserManager<ApiUser> userManager,IConfiguration configuration)
		{
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

        public async Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDto userDto)
        {
            _user = _mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;

			var result = await _userManager.CreateAsync(_user, userDto.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(_user, "User");
			}

			return result.Errors;
        }

		public async Task<AuthResponseDto> Login(LoginDto loginDto)
		{
			bool IsValidUSer = false;


            _user = await _userManager.FindByEmailAsync(loginDto.Email);
			IsValidUSer = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

			if (_user == null ||IsValidUSer ==false)
			{
				return null;
			}
			var token = await GenrateToken();

			return new AuthResponseDto
			{
				Token = token,
				UserId = _user.Id,
				RefreshToken = await CreateRefreshToken()
			};

		}
		private async Task<string> GenrateToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSwttings:Key"]));
			var credetials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var roles = await _userManager.GetRolesAsync(_user);

			var rolesCliams = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
			var userCliams = await _userManager.GetClaimsAsync(_user);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub,_user.Email),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email,_user.Email)
			}.Union(userCliams).Union(rolesCliams);

			var token = new JwtSecurityToken(

				issuer :_configuration["JwtSwttings:Issuer"],
				audience : _configuration["JwtSwttings:Audience"],
				claims : claims,
				expires : DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSwttings:DurationInMinutes"])),
				signingCredentials : credetials
                );



			return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<string> CreateRefreshToken()
        {
			await _userManager.RemoveAuthenticationTokenAsync(_user,_loginProvider,_refreshToken);
			var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);

            var result  = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
			return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

			var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
			_user = await _userManager.FindByEmailAsync(username);

			if (_user ==null || _user.Id != request.UserId)
			{
				return null;
			}

			var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

			if (isValidRefreshToken)
			{
				var token = await GenrateToken();

				return new AuthResponseDto
				{
					Token = token,
					UserId = _user.Id,
					RefreshToken = await CreateRefreshToken()
				};
			}

			await _userManager.UpdateSecurityStampAsync(_user);
			return null;
        }
    }
}

