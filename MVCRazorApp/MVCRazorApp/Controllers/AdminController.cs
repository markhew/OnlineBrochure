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
        public ActionResult Index()
        {
            return View ();
        }

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register([Bind(Include = "Username,Email,Password,ConfirmPassword")] AdminViewModel admin)
		{
			if (ModelState.IsValid) 
			{
				using (var db = new AdminDBContext())
				{
					using (var sha = SHA256.Create()) 
					{
						var password = "";
					}
					db.SaveChanges();
				}
				ModelState.Clear();
				ViewBag.Message = "Admin :" + admin.Username + " created.";
			}


			return RedirectToAction("Index");


		}


	}
}
