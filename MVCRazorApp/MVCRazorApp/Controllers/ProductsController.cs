using System;
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
			ProductViewModel model = new ProductViewModel();
			model.Products = db.Products.ToList();
			model.Categories = db.Categories.ToList();
			//model.Categories = db.Categories.ToList();
			return View(model.Products);
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
			return View();
		}

		// POST: /Product/Create 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include="Name,Category,Description,Price")] Product product, HttpPostedFileBase Image)
		{
			if (ModelState.IsValid)
			{

				if (Image != null)
				{
					byte[] imgByte = null;

					using (var binaryReader = new BinaryReader(Image.InputStream))
					{
						imgByte = binaryReader.ReadBytes(Image.ContentLength);
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

		// GET: /Product/Edit/5 
		public ActionResult Edit(int? id)
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

		// POST: /Product/Edit/5 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int ID, string Name, int Category, string Description, decimal Price, HttpPostedFileBase Image)
		{
			Product product = db.Products.Find(ID);



			if (ModelState.IsValid)
			{
				product.Name = Name;
				product.Category = Category;
				product.Description = Description;
				product.Price = Price;

				if (Image != null)
				{
					byte[] imgByte;
					using (var binaryReader = new BinaryReader(Image.InputStream))
					{
						imgByte = binaryReader.ReadBytes(Image.ContentLength);
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
				graph.FillRectangle(Brushes.White, ImageSize);
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

	}
}
