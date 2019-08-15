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
    public class AdminSellerRequestsController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: SellerRequests
        public ActionResult Index()
        {
            var sellerRequest = db.SellerRequest.Include(s => s.Seller);
            return View(sellerRequest.ToList());
        }

        // GET: SellerRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = db.Address.Where(a => a.Seller_ID == id).FirstOrDefault();
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }
        

       

        // GET: SellerRequests/Accept/5
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerRequest sellerRequest = db.SellerRequest.Find(id);
            if (sellerRequest == null)
            {
                return HttpNotFound();
            }
            return View(sellerRequest);
        }

        // POST: SellerRequests/Accept/5
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            SellerRequest sellerRequest = db.SellerRequest.Find(id);
            db.SellerRequest.Remove(sellerRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SellerRequests/Accept/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerRequest sellerRequest = db.SellerRequest.Find(id);
            if (sellerRequest == null)
            {
                return HttpNotFound();
            }
            return View(sellerRequest);
        }

        // POST: SellerRequests/Accept/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int seller_ID)
        {
            SellerRequest sellerRequest = db.SellerRequest.Find(id);
            Seller seller = db.Seller.Find(seller_ID);
            var address = db.Address.Where(a => a.Seller_ID == seller_ID).FirstOrDefault();
            db.Address.Remove(address);
            db.SellerRequest.Remove(sellerRequest);
            db.Seller.Remove(seller);
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
    }
}
