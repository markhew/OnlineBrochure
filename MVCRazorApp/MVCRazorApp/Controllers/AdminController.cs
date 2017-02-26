using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MVCRazorApp.Models;

namespace MVCRazorApp.Controllers
{
    public class AdminController : Controller
    {
		private AdminDBContext db = new AdminDBContext();
        public ActionResult Index()
        {
			return View (db.Admins.ToList());
        }

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register([Bind(Include = "Username,Email,Password,ConfirmPassword")] AdminViewModel viewAdmin)
		{
			if (ModelState.IsValid) 
			{
				
				byte[] salt = AdminHelper.GenerateSalt();
				string hashPassword = AdminHelper.HashPassword(viewAdmin.Password, salt);
				var newAdmin = new Admin(viewAdmin.Username, viewAdmin.Email, hashPassword, salt, DateTime.Now);

				db.Admins.Add(newAdmin);
				db.SaveChanges();

				ModelState.Clear();
				ViewBag.Message = "Admin :" + viewAdmin.Username + " created.";
			}


			return RedirectToAction("Index");


		}

		public ActionResult Login()
		{
			return View();

		}

		[HttpPost]
		public ActionResult Login([Bind(Include = "Username,Password")] LoginViewModel user)
		{
			var usr = db.Admins.Where(u => u.Username == user.Username &&
			                          AdminHelper.ComparePassword(u.Password, u.Salt, user.Password)).FirstOrDefault();

			if (usr != null)
			{
				Session["Username"] = usr.Username;
				return RedirectToAction("Index", "Product");
			}
			else 
			{
				ModelState.AddModelError("", "Incorrect Username or Password");
			}
			return View();
		}



	}
}
