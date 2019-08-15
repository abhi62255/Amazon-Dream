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
    public class SellerProductViewController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: SellerProductView
        public ActionResult Index()
        {
            var id = Convert.ToInt32(Session["SellerID"]);
            var modelP = db.Product.Where(p => p.Seller_ID == id).ToList();
            var modelPR = db.ProductRequest.Select(p => p.Product_ID).ToList();

            foreach (var i in modelPR)
            {
                var pro = db.Product.Where(p => p.ID == i).FirstOrDefault();
                modelP.Remove(pro);
            }
            return View(modelP);
        }

        // GET: AdminProductTrend/Edit/5
        public ActionResult Edit(long? id)
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

        // POST: AdminProductTrend/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.Seller_ID = Convert.ToInt32(Session["SellerID"]);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }



        // GET: SellerProductView/Delete/5
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

        // POST: SellerProductView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PendingProductRequest()
        {
            var id = Convert.ToInt32(Session["SellerID"]);
            List<Product> modelP = new List<Product>();
            var modelPR = db.ProductRequest.Select(p => p.Product_ID).ToList();

            foreach (var i in modelPR)
            {
                var pro = db.Product.Where(p => p.ID == i && p.Seller_ID ==id).FirstOrDefault();
                modelP.Add(pro);
            }
            return View(modelP);
        }

        // GET: AdminProductTrend/Edit/5
        public ActionResult EditPR(long? id)
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

        // POST: AdminProductTrend/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPR(Product model)
        {
            if (ModelState.IsValid)
            {
                model.Seller_ID = Convert.ToInt32(Session["SellerID"]);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PendingProductRequest");
            }
            return View(model);
        }



        // GET: SellerProductView/Delete/5
        public ActionResult DeletePR(long? id)
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

        // POST: SellerProductView/Delete/5
        [HttpPost, ActionName("DeletePR")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedPR(long id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("PendingProductRequest");
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
