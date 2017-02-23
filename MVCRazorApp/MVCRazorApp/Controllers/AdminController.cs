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
				using (var sha = SHA256.Create()) 
				{
					var password = "";
				}
				byte[] salt = AdminHelper.GenerateSalt();

				Admin newAdmin = new Admin(viewAdmin.Username, viewAdmin.Email, viewAdmin.Password, salt, DateTime.Now);

				db.Admins.Add(newAdmin);
				db.SaveChanges();

				ModelState.Clear();
				ViewBag.Message = "Admin :" + viewAdmin.Username + " created.";
			}


			return RedirectToAction("Index");


		}


	}
}
