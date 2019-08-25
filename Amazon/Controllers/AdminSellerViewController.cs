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
            var modelDS = db.DeletedSeller.Select(s => s.Seller).ToList();
            foreach(var seller in modelSR)
            {
                modelS.Remove(seller);
            }
            foreach (var seller in modelDS)
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
            var modelDP = new DeletedProduct();
            var modelDS = new DeletedSeller();
            modelDS.Seller_ID = id;
            db.DeletedSeller.Add(modelDS);
            var modelP = db.Product.Where(p => p.Seller_ID == id).ToList();
            var modelPR = db.ProductRequest.Where(p => p.Product.Seller_ID == id).Select(p => p.Product).ToList();
            foreach(var pro in modelPR)     //deleting product which are not approved by admin
            {
                modelP.Remove(pro);
                //var model = db.ProductRequest.Where(p => p.ID == pro.ID).FirstOrDefault();
                db.Product.Remove(pro);
                db.SaveChanges();
            }

            foreach (var pro in modelP)
            {
                pro.ProductQuantity = 0;
                db.Entry(pro).State = EntityState.Modified;
                modelDP.Product_ID = pro.ID;
                var modelT = db.Trend.Where(p => p.Product_ID == pro.ID).FirstOrDefault();      //removing product from Trend table
                if(modelT != null)
                {
                    db.Trend.Remove(modelT);
                }
                var modelTR = db.TrendRequest.Where(p => p.Product_ID == pro.ID).FirstOrDefault();       //removing product from TrendRequest table
                if (modelTR != null)
                {
                    db.TrendRequest.Remove(modelTR);
                }

                db.DeletedProduct.Add(modelDP);
                db.SaveChanges();


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
