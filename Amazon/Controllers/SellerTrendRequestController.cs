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
    [Authorize(Users = "SELLER")]
    public class SellerTrendRequestController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: SellerTrendRequestS
        public ActionResult Index()
        {
            var modelTRR = new List<TrendRequest>();
            var id = Convert.ToInt32(Session["SellerID"]);
            var modelP = db.Product.Where(p => p.Seller_ID == id).Select(p=>p.ID).ToList();
            var modelTR = db.TrendRequest.Select(p => p.Product_ID).ToList();//need refactor

            foreach(var i in modelP)
            {
                foreach(var j in modelTR)
                {
                    if(i==j)
                    {
                        var pro = db.TrendRequest.Where(t => t.Product_ID == j).FirstOrDefault();
                        modelTRR.Add(pro);
                    }
                }
            }

            var trendRequest = db.TrendRequest.Include(t => t.Product);

            return View(modelTRR.ToList());
        }

      

        // GET: SellerTrendRequest/Create
        public ActionResult Create()
        {
            var id = Convert.ToInt32(Session["SellerID"]);
            var product = db.Product.Where(p=>p.Seller_ID == id).ToList();
            var modelT = db.Trend.Select(t => t.Product_ID).ToList();
            var modelTR = db.TrendRequest.Select(t => t.Product_ID).ToList();
            var modelPR = db.ProductRequest.Select(t => t.Product_ID).ToList();

            foreach (var i in modelT)
            {
                var pro = db.Product.Where(p => p.ID == i).FirstOrDefault();
                product.Remove(pro);
            }
            foreach (var i in modelTR)
            {
                var pro = db.Product.Where(p => p.ID == i).FirstOrDefault();
                product.Remove(pro);
            }
            foreach (var i in modelPR)
            {
                var pro = db.Product.Where(p => p.ID == i).FirstOrDefault();
                product.Remove(pro);
            }
            ViewBag.Product_ID = new SelectList(product, "ID", "ProductName");
            return View();
        }

        // POST: SellerTrendRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Product_ID")] TrendRequest trendRequest)
        {
            if (ModelState.IsValid)
            {
                db.TrendRequest.Add(trendRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_ID = new SelectList(db.Product, "ID", "ProductName", trendRequest.Product_ID);
            return View(trendRequest);
        }

      

        // GET: SellerTrendRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrendRequest trendRequest = db.TrendRequest.Find(id);
            if (trendRequest == null)
            {
                return HttpNotFound();
            }
            return View(trendRequest);
        }

        // POST: SellerTrendRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrendRequest trendRequest = db.TrendRequest.Find(id);
            db.TrendRequest.Remove(trendRequest);
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
