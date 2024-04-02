using System;
using Microsoft.AspNetCore.Identity;

namespace HotelListning.Data
{
	public class ApiUser : IdentityUser
    {
		public string FirstName  { get; set; }
		public string  LastName { get; set; }
	}
}

