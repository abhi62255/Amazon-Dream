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
        public ActionResult Index(string searchTerm = null)
        {
            var product = db.Product.Where(p => searchTerm == null || p.ProductName.StartsWith(searchTerm))
                .Include(p => p.Seller).ToList();
            var modelPR = db.ProductRequest.Select(p => p.Product).ToList();
            var modelDP = db.DeletedProduct.Select(p => p.Product_ID).ToList();

            foreach (var pro in modelPR)
            {
                product.Remove(pro);
            }
            foreach (var pro in modelDP)
            {
                var model = db.Product.Where(p => p.ID == pro).FirstOrDefault();
                product.Remove(model);
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
            var modelDP = new DeletedProduct();     //adding product to deletedProduct table
            modelDP.Product_ID = id;
            db.DeletedProduct.Add(modelDP);

            Product product = db.Product.Find(id);          //making quantity zero of deleted product
            product.ProductQuantity = 0;
            db.Entry(product).State = EntityState.Modified;


            var modelT = db.Trend.Where(t => t.Product_ID == id).FirstOrDefault();      //remove from trending
            if (modelT != null)
            {
                db.Trend.Remove(modelT);
            }

            var modelTR = db.TrendRequest.Where(t => t.Product_ID == id).FirstOrDefault();      //remove from trend Request
            if (modelTR != null)
            {
                db.TrendRequest.Remove(modelTR);
            }
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
