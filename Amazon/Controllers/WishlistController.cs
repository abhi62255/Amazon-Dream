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
    public class WishlistController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: Wishlist
        public ActionResult Index()
        {
            var id = Convert.ToInt64(Session["CustomerID"]);
            var wishlist = db.Wishlist.Where(w=>w.Customer_ID == id);
            return View(wishlist.ToList());
        }

        // GET: Wishlist/ProductDetails/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlist.Find(id);
            if (wishlist != null)
            {
                return RedirectToAction("ProductDetail", "Home", new { wishlist.Product_ID });
            }
            return RedirectToAction("Index");
        }

        // GET: Wishlist/Create
        public ActionResult Add(long? Product_ID)
        {
            var id = Convert.ToInt32(Session["CustomerID"]);

            var model = db.Kart.Where(o => o.Product_ID == Product_ID && o.Customer_ID == id).FirstOrDefault();
            if(model == null)
            {
                var modelW = new Wishlist();
                modelW.Product_ID = Convert.ToInt64(Product_ID);
                modelW.Customer_ID = id;

                db.Wishlist.Add(modelW);
                db.SaveChanges();
            }
            
            return RedirectToAction("ProductDetail","Home", new { Product_ID });
        }


        // GET: Wishlist/Delete/5
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlist.Find(id);
            if (wishlist != null)
            {
                db.Wishlist.Remove(wishlist);
                db.SaveChanges();
            }
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
    }
}
