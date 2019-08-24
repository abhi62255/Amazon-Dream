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
    public class ProductFeedbackController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: ProductFeedback
        public ActionResult Index()
        {
            var feedbacks = db.Feedback.Include(f => f.Customer).Include(f => f.Product);
            return View(feedbacks.ToList());
        }


        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Rating,Review,Product_ID,Customer_ID")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedback.Add(feedback);
                db.SaveChanges();
            }
            return RedirectToAction("ProductDetail", "Home", new { Product_ID = feedback.Product_ID });
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
