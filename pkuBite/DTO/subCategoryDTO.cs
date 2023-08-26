using System;
using pkuBite.Models;

namespace pkuBite.DTO
{
	public class subCategoryDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }

		IEnumerable<Category> categories { get; set; }
	}
}

