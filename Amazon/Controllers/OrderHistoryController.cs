using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Amazon;
using Amazon.Models;

namespace Amazon.Controllers
{
    public class OrderHistoryController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: OrderHistory
        public ActionResult UserOrderHistory()
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var orderPlaced = db.OrderPlaced.Where(o =>o.Customer_ID == id).Include(o => o.Product);
            return View(orderPlaced.ToList());
        }

        public ActionResult SellerOrderRecived()
        {
            var id = Convert.ToInt32(Session["SellerID"]);
            var orderPlaced = db.OrderPlaced.Where(o =>o.Product.Seller_ID == id).Include(o => o.Product);
            return View(orderPlaced.ToList());
        }

        public ActionResult AdminOrderHistory()
        {
            var orderPlaced = db.OrderPlaced.Include(o => o.Product);
            return View(orderPlaced.ToList());
        }

        [HttpGet]
        public ActionResult UpdateStatus(int id)
        {
            var model = db.OrderPlaced.Where(o => o.ID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateStatus(OrderPlaced model,int id)
        {
            var modelOP = db.OrderPlaced.Where(o => o.ID == id).FirstOrDefault();
            modelOP.Status = model.Status;
            db.Entry(modelOP).State = EntityState.Modified;
            db.SaveChanges();

            if(User.Identity.Name == "SELLER")
            {
                return RedirectToAction("SellerOrderRecived");
            }
            return RedirectToAction("AdminOrderHistory");

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
