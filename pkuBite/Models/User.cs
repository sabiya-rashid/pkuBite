using System;
using System.ComponentModel.DataAnnotations;

namespace pkuBite.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string email { get; set; }

		[Required]
		public string passwordHash { get; set; }

		public string Role { get; set; } = "User";
	}
}

