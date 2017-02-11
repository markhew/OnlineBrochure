using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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

		public CreateProductViewModel(){}

	}

	public class EditProductViewModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Category { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Image OldImage { get; set; }

		public IEnumerable<Category> Categories { get; set; }
		[DisplayName("Upload New Image")]
		public HttpPostedFileBase NewImage { get; set; }

		public EditProductViewModel() { }
		public EditProductViewModel(Product p, IEnumerable<Category> c)
		{
			this.ID = p.PID;
			this.Name = p.Name;
			this.Category = p.Category;
			this.Description = p.Description;
			this.Price = p.Price;

			var ms = new MemoryStream(p.Image);
			this.OldImage = Image.FromStream(ms);

			this.Categories = c;
		}


	}
}
