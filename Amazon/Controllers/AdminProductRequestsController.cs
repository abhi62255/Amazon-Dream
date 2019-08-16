﻿using System;
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
    public class AdminProductRequestsController : Controller
    {
        private AKARTDBContext db = new AKARTDBContext();

        // GET: AdminProductRequests
        public ActionResult Index()
        {
            var productRequest = db.ProductRequest.Include(p => p.Product);
            return View(productRequest.ToList());
        }

        // GET: AdminProductRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Where(p => p.ID == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        

       

        // GET: AdminProductRequests/Delete/5
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRequest productRequest = db.ProductRequest.Find(id);
            if (productRequest == null)
            {
                return HttpNotFound();
            }
            return View(productRequest);
        }

        // POST: AdminProductRequests/Delete/5
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptConfirmed(int id)
        {
            ProductRequest productRequest = db.ProductRequest.Find(id);
            db.ProductRequest.Remove(productRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: AdminProductRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRequest productRequest = db.ProductRequest.Find(id);
            if (productRequest == null)
            {
                return HttpNotFound();
            }
            return View(productRequest);
        }

        // POST: AdminProductRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Product_ID)
        {
            Product product = db.Product.Find(Product_ID);
            db.Product.Remove(product);
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
