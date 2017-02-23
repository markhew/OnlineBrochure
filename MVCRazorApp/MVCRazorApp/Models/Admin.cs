using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCRazorApp.Models
{
	[Table("Admin")]
	public class Admin
	{
		[Key]
		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		public byte[] Salt { get; set; }

		public DateTime Create_time { get; set; }

		public Admin() {}

		public Admin(string uname, string email, string password, byte[] salt, DateTime ctime)
		{
			this.Username = uname;
			this.Email = email;
			this.Password = password;
			this.Create_time = ctime;
			this.Salt = salt;
		}
	}

	public partial class AdminDBContext : DbContext
	{
		public AdminDBContext() : base(nameOrConnectionString: "DatabaseContext") { }

		public DbSet<Admin> Admins { get; set; }
	}
}
