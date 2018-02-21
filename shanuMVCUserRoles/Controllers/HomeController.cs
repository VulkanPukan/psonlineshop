using shanuMVCUserRoles.EntityModels;
using shanuMVCUserRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shanuMVCUserRoles.Controllers
{
	public class HomeController : Controller
	{
        private ProductContext db = new ProductContext();
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            List<ProductModel> pr = db.Products.ToList();
            int pageSize = 12; // количество объектов на страницу
            IEnumerable<ProductModel> phonesPerPages = pr.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Products.Count() };
            PagingModel ivm = new PagingModel { PageInfo = pageInfo, Phones = phonesPerPages };
            return View(ivm);

        }

       

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        public ActionResult Cart()
        {
            List<int> carProducts = (List<int>)this.Session["ids"];
            List<ProductModel> products = new List<ProductModel>();
            if (carProducts == null || carProducts.Count == 0)
            {
                return View(products);
            }
            else
            {
                foreach (var item in carProducts)
                    products.Add(db.Products.Where(x => x.id == item).First());
                return View(products);
            }
        } 
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult DeleteFromCart(int id)
        {
            ((List<int>)this.Session["ids"]).Remove(id);
            return RedirectToAction("Cart");
        }

       public ActionResult Search(string word)
        {
            ViewBag.word = word;
            var prodSearch = db.Products.Where(a => a.title.Contains(word)).ToList();
            return View(prodSearch);
        }

    }
}