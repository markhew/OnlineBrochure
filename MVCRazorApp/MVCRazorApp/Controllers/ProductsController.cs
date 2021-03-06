﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Net;

using MVCRazorApp.Models;
using System.IO;
using System.Drawing;
namespace MVCRazorApp.Controllers
{
	public class ProductsController : Controller
	{

		private BrochureDBContext db = new BrochureDBContext();

		// GET: /Products/ 
		public ActionResult Index()
		{
			return View(db.Products.ToList());
		}


		// GET: /Movies/Details/5 
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: /Products/Create 
		public ActionResult Create()
		{
			CreateProductViewModel viewmodel = new CreateProductViewModel();
			viewmodel.Categories = db.Categories.ToList();
			return View(viewmodel);
		}

		public ActionResult CreateCategory()
		{
			return View();
		}

		// POST: /Product/Create 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include="Name,Category,Description,Price,Image")] CreateProductViewModel productmodel)
		{
			Product product = new Product(productmodel);

			if (ModelState.IsValid)
			{

				if (productmodel.Image != null)
				{
					byte[] imgByte = null;

					using (var binaryReader = new BinaryReader(productmodel.Image.InputStream))
					{
						imgByte = binaryReader.ReadBytes(productmodel.Image.ContentLength);
					}
					product.Image = imgByte;

				}
				else {
					product.Image = UseDefaultImage();
				}



				db.Products.Add(product);

				db.SaveChanges();
				return RedirectToAction("Index");
			}


			return View(product);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateCategory([Bind(Include = "Name,Description")] Category cat)
		{
			if (ModelState.IsValid)
			{
				db.Categories.Add(cat);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(cat);
		}


		// GET: /Product/Edit/5 
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			var viewmodel = new EditProductViewModel(product,db.Categories.ToList());

			if (product == null)
			{
				return HttpNotFound();
			}
			return View(viewmodel);
		}

		public ActionResult Edit2(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			var viewmodel = new EditProductViewModel(product, db.Categories.ToList());

			if (product == null)
			{
				return HttpNotFound();
			}
			return View(viewmodel);
		}

		// POST: /Product/Edit/5 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,Category,Description,Price,Image")] EditProductViewModel productmodel)
		{
			Product product = db.Products.Find(productmodel.ID);

			if (ModelState.IsValid)
			{
				product.Name = productmodel.Name;
				product.Category = productmodel.Category;
				product.Description = productmodel.Description;
				product.Price = productmodel.Price;

				if (productmodel.NewImage != null)
				{
					byte[] imgByte;
					using (var binaryReader = new BinaryReader(productmodel.NewImage.InputStream))
					{
						imgByte = binaryReader.ReadBytes(productmodel.NewImage.ContentLength);
					}
					product.Image = imgByte;

				}

				db.Entry(product).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(product);
		}

		// GET: /Movies/Delete/5 
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: /Product/Delete/5 
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = db.Products.Find(id);
			db.Products.Remove(product);
			db.SaveChanges();
			return RedirectToAction("Index");
		}


		public FileContentResult getImg(int id)
		{
			byte[] byteArray = db.Products.Find(id).Image;
			return byteArray != null
				? new FileContentResult(byteArray, "image/jpeg")
				: null;
		}



		private byte[] UseDefaultImage()
		{
			int width = 800;
			int height = 800;

			Bitmap img = new Bitmap(width, height);
			using (Graphics graph = Graphics.FromImage(img))
			{
				Rectangle ImageSize = new Rectangle(0, 0, width, height);
				graph.FillRectangle(Brushes.Black, ImageSize);
			}

			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(img, typeof(byte[]));

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		public ActionResult GetCategory(int catID)
		{
			return Content(db.Categories.Find(catID).Name);
		}

		public ActionResult GetCategoryDesc(int catID)
		{
			return Content(db.Categories.Find(catID).Description);
		}

	}
}
