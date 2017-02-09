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

		private ProductDBContext db = new ProductDBContext();

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
			return View();
		}

		// POST: /Product/Create 
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for  
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include="Name,Type,Description,Price")] Product product, HttpPostedFileBase ProductImg)
		{
			if (ModelState.IsValid)
			{

				if (ProductImg != null)
				{
					byte[] imgByte = null;

					using (var binaryReader = new BinaryReader(ProductImg.InputStream))
					{
						imgByte = binaryReader.ReadBytes(ProductImg.ContentLength);
					}
					product.ProductImg = imgByte;

				}
				else {
					product.ProductImg = UseDefaultImage();
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
		public ActionResult Edit(int ID, string Name, string Type, string Description, decimal Price, HttpPostedFileBase ProductImg)
		{
			Product product = db.Products.Find(ID);



			if (ModelState.IsValid)
			{
				product.Name = Name;
				product.Type = Type;
				product.Description = Description;
				product.Price = Price;

				if (ProductImg != null)
				{
					byte[] imgByte;
					using (var binaryReader = new BinaryReader(ProductImg.InputStream))
					{
						imgByte = binaryReader.ReadBytes(ProductImg.ContentLength);
					}
					product.ProductImg = imgByte;

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
			byte[] byteArray = db.Products.Find(id).ProductImg;
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
