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
    public class AdminProductViewController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: AdminProductView
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.Seller).ToList();
            var modelPR = db.ProductRequest.Select(p => p.Product).ToList();

            foreach(var pro in modelPR)
            {
                product.Remove(pro);
            }


            return View(product.ToList());
        }
        
        // GET: AdminProductView/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminProductView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
