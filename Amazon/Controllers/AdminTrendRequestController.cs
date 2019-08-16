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
    public class AdminTrendRequestController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: AdminTrendRequest
        public ActionResult Index()
        {
            var trendRequest = db.TrendRequest.Include(t => t.Product);
            return View(trendRequest.ToList());
        }


        // GET: AdminTrendRequest/Delete/5
        public ActionResult Accept(int? id)
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

        // POST: AdminTrendRequest/Delete/5
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            TrendRequest trendRequest = db.TrendRequest.Find(id);
            var modelT = new Trend();
            modelT.Product_ID = trendRequest.Product_ID;
            db.Trend.Add(modelT);
            db.TrendRequest.Remove(trendRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: AdminTrendRequest/Delete/5
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

        // POST: AdminTrendRequest/Delete/5
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
