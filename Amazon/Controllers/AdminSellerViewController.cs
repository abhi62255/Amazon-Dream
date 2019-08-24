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
    [Authorize(Users = "ADMIN")]
    public class AdminSellerViewController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: AdminSellerView
        public ActionResult Index(string searchTerm = null)
        {
            var modelS = db.Seller.Where(p => searchTerm == null || p.SellerName.StartsWith(searchTerm)).ToList();

            var modelSR = db.SellerRequest.Select(s=>s.Seller).ToList();
            foreach(var seller in modelSR)
            {
                modelS.Remove(seller);
            }

            return View(modelS);
        }


        // GET: AdminSellerView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Seller.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: AdminSellerView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Seller.Find(id);
            var modelA = db.Address.Where(a => a.Seller_ID == seller.ID).FirstOrDefault();
            db.Address.Remove(modelA);
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
