using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;
using MVCRazorApp.Models;

namespace MVCRazorApp.Models
{
	public class CreateProductViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public int Category { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
		public decimal Price { get; set; } 

		public HttpPostedFileBase Image { get; set; }
		public IEnumerable<Category> Categories { get; set; }

		public CreateProductViewModel()
		{
		}
	}
}
