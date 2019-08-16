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
    public class SellerProductInTrendController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: SellerProductInTrend
        public ActionResult Index()
        {
            var trend = db.Trend.Select(t => t.Product).ToList();
            var modelP = new List<Product>();


            int id = Convert.ToInt32(Session["SellerID"]);
            foreach(var pro in trend)
            {
                if(pro.Seller_ID ==  id)
                {
                    modelP.Add(pro);
                }
            }
            return View(modelP);
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
