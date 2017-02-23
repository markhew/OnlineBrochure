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


	}
}
