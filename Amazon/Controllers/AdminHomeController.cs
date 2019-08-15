using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon.Controllers
{
    [Authorize(Users ="ADMIN")]
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SellerVerification()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult SellerVerification()
        //{

        //}

    }
}
