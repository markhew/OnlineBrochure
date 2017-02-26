using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MVCRazorApp;
using MVCRazorApp.Controllers;

namespace MVCRazorApp.Tests
{
	[TestFixture]
	public class AdminControllerTest
	{
		[Test]
		public void SaltTest() 
		{
			byte[] salt = AdminHelper.GenerateSalt();
			Assert.NotNull(salt);
			Assert.AreEqual(64, salt.Count(), "Salt not of Length 64");
		}

		[Test]
		public void TestStringToByte()
		{
			string sPassword = "StringPassword";
			byte[] bPassword = Encoding.ASCII.GetBytes(sPassword);
			string sPassword2 = Encoding.ASCII.GetString(bPassword);
			Assert.AreEqual(sPassword, sPassword2);
		}


		[Test]
		public void TestSaltHash()
		{
			string password = "StringPassword";
			byte[] salt = AdminHelper.GenerateSalt();
			string hPassword = AdminHelper.HashPassword(password, salt);

			Assert.AreNotSame(password, hPassword,"Passwords are the same");
			Assert.AreEqual(hPassword.Length, 48, "Hashed Password Length not 48");

			string password2 = "ssssssStringPasswordddddddd";
			byte[] salt2 = AdminHelper.GenerateSalt();
			string hPassword2 = AdminHelper.HashPassword(password, salt2);

			Assert.AreNotSame(password2, hPassword2, "Passwords are the same");
			Assert.AreEqual(hPassword.Length, 48, "Hashed Password Length not 48");

		}
	}
}
