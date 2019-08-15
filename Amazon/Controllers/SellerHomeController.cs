using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon.Controllers
{
    [Authorize(Users ="SELLER")]
    public class SellerHomeController : Controller
    {
        // GET: SellerHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: SellerHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellerHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerHome/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellerHome/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
