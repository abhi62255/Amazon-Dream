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
    public class AdminProductTrendController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: AdminProductTrend
        public ActionResult Index()
        {
            var trend = db.Trend.Include(t => t.Product);
            return View(trend.ToList());
        }


        // GET: AdminProductTrend/Create
        public ActionResult Create()
        {
            var modelP = db.Product.ToList();
            var modelPR = db.ProductRequest.Select(p => p.Product).ToList();
            var modelT = db.Trend.Select(p => p.Product).ToList();

            foreach (var pro in modelPR){ modelP.Remove(pro); }     //removing product which are not approved
            foreach (var pro in modelT) { modelP.Remove(pro); }     //removing product which are already in Trend



            ViewBag.Product_ID = new SelectList(modelP, "ID", "ProductName");
            return View();
        }

        // POST: AdminProductTrend/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Product_ID")] Trend trend)
        {
            if (ModelState.IsValid)
            {
                db.Trend.Add(trend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_ID = new SelectList(db.Product, "ID", "ProductName", trend.Product_ID);
            return View(trend);
        }

        
        // GET: AdminProductTrend/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trend trend = db.Trend.Find(id);
            if (trend == null)
            {
                return HttpNotFound();
            }
            return View(trend);
        }

        // POST: AdminProductTrend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trend trend = db.Trend.Find(id);
            db.Trend.Remove(trend);
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
