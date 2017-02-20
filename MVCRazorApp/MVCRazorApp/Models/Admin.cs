using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCRazorApp.Models
{
	public class Admin
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		public DateTime Create_time { get; set; }

		public Admin() 
		{
			

		}
	}

	public partial class AdminDBContext : DbContext
	{
		public AdminDBContext() : base(nameOrConnectionString: "DatabaseContext") { }

		public DbSet<Admin> Admins { get; set; }
	}
}
