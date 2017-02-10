using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCRazorApp.Models
{
	[Table("Product")]
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("ID")]
		public int PID { get; set; }
		[Required]
		public string Name { get; set; }

		[Required]
		public int Category { get; set; }

		[Required]
		public string Description { get; set; }

		public decimal Price { get; set; }

		public byte[] Image { get; set; }



		[ForeignKey("Category")]
		public virtual Category Cat { get; set; }



	}

	[Table("Category")]
	public class Category 
	{ 
		[Key]
		public int CID { get; set; }

		[Required]
		[StringLength(50, ErrorMessage="Name cannot exceed 50 characters")]
		public string Name { get; set; }

		[Required]
		[StringLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
		public string Description { get; set; }

	}



	public partial class BrochureDBContext : DbContext
	{
		public BrochureDBContext() : base(nameOrConnectionString: "DatabaseContext") { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
	}

}
