﻿using System;
namespace pkuBite.DTO
{
	public class UserDTO
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string? Role { get; set; }
	}
}

