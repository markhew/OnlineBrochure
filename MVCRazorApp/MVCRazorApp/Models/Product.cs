using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCRazorApp.Models
{
	public class Product
	{
		
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Type { get; set; }
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }
		[DisplayName("Upload Image")]
		public byte[] ProductImg { get; set; }




	}

	public partial class ProductDBContext : DbContext
	{
		public ProductDBContext() : base(nameOrConnectionString: "MyDatabaseContext") { }

		public DbSet<Product> Products { get; set; }

	}
}
