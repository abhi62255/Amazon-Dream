﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon.Controllers
{
    public class PreVisitController : Controller
    {
        AKARTDBContext _db = new AKARTDBContext();
        // GET: PreVisit
        public ActionResult Index()
        {
            var id = Convert.ToInt64(Session["CustomerID"]);
            var preVisit = _db.PreVisit.Where(w => w.Customer_ID == id);
            return View(preVisit.ToList());
        }


        public ActionResult Details(int? id)
        {
            var preVisit = _db.PreVisit.Find(id);
            if (preVisit != null)
            {
                return RedirectToAction("ProductDetail", "Home", new { preVisit.Product_ID });
            }
            return RedirectToAction("Index");
        }

        // GET: Wishlist/Delete/5
        public ActionResult Remove(int? id)
        {

            var preVisit = _db.PreVisit.Find(id);
            if (preVisit != null)
            {
                _db.PreVisit.Remove(preVisit);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
