using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles.EntityModels;
using shanuMVCUserRoles.Models;

namespace shanuMVCUserRoles.Controllers
{
    public class ProductModelsController : Controller
    {
        private const string _ImagesPath = "~/Image/";
        private ProductContext db = new ProductContext();

        // GET: ProductModels
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: ProductModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // GET: ProductModels/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel productModel, HttpPostedFileBase Image)
        {
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
            //ImageFile.SaveAs(Guid.NewGuid() + ".jpg");
            if (Image == null)
            {
                ModelState.AddModelError("Image", "Please upload image");
            }
            if (ModelState.IsValid)
            {
               
                if (Image != null)
                {
                    if (ImageExtensions.Contains(Path.GetExtension(Image.FileName).ToUpperInvariant()))
                    {
                        Path.GetExtension(Image.FileName);
                        var fileName = Guid.NewGuid().ToString() + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                        var pathForSave = "~/Image/" + fileName;
                        Image.SaveAs(path);
                        productModel.imagePath = pathForSave;

                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Please upload image");
                    }


                }
                db.Products.Add(productModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productModel);

        }

        // GET: ProductModels/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ProductModel productModel,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    Path.GetExtension(Image.FileName);
                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    var pathForSave = "~/Image/" + fileName;
                    Image.SaveAs(path);
                    productModel.imagePath = pathForSave;

                }
                db.Products.Attach(productModel);
                db.Entry(productModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productModel);
        }

        // GET: ProductModels/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: ProductModels/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productModel = db.Products.Find(id);
            db.Products.Remove(productModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public ActionResult AddCart(int id)
        {
            if (((List<int>)this.Session["ids"]) != null)
            {
                ((List<int>)this.Session["ids"]).Add(id);
                return RedirectToAction("Cart","Home","");
            }
            else
            {
                List<int> myList = new List<int>();
                this.Session["ids"] = myList;
                ((List<int>)this.Session["ids"]).Add(id);
                return RedirectToAction("Cart", "Home", "");
            }


        }
    }
}
