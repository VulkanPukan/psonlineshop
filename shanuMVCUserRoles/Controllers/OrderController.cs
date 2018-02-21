using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shanuMVCUserRoles.Models;
using shanuMVCUserRoles.EntityModels;
using System.Threading;

namespace shanuMVCUserRoles.Controllers
{
    public class OrderController : Controller
    {
        ProductContext pc = new ProductContext();
        OrderContext db = new OrderContext();
        // GET: Order
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Index()
        {
            var a = db.Orders.ToList();

            return View(a);
        }

        public ActionResult AddOrder(List<int> Orders, string Name, string Phone, string Mail, double Count)
        {
            OrderModel or = new OrderModel();
            List<ProductModel> pr = new List<ProductModel>();
            foreach (var item in Orders)
            {
                var b = pc.Products.Where(x => x.id == item).First();
                b.id = item;
                pr.Add(b);
            }
            or.products = pr;
            or.customerName = Name;
            or.customerNumber = Phone;
            or.customerMail = Mail;
            or.total = Count;
            db.Orders.Add(or);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var or = db.Orders.Where(x => x.id == id).First();
            return View(or);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Posted(int id)
        {
          
            var del = db.Orders.Where(x => x.id == id).First();


            for (int i = 0; i < del.products.Count; i++)
            {
               
                del.products.ToList().RemoveAt(i);
                Thread.Sleep(100);
            }
           
            // delete the author
            db.Orders.Remove(del);

            db.SaveChanges();

            
            return RedirectToAction("Index", "Order");
        }
    }
}