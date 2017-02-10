using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using MVCRazorApp.Models;

namespace MVCRazorApp.Models
{
	public class ProductViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }

		public ProductViewModel()
		{
			this.Products = new List<Product>();
			this.Categories = new List<Category>();
		}
	}
}
