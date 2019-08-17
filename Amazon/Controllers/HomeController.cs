using Amazon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazon.Controllers
{
    public class HomeController : Controller
    {
        AKARTDBContext _db = new AKARTDBContext();
        public ActionResult Index(string searchTerm = null)
        {
            var modelP = _db.Product.Where(p => searchTerm == null || p.ProductName.StartsWith(searchTerm))
                .ToList();
            var modelPR = _db.ProductRequest.Select(p => p.Product_ID).ToList();

            foreach(var pro in modelPR)
            {
                var model = _db.Product.Where(p => p.ID == pro).FirstOrDefault();
                modelP.Remove(model);
            }


            return View(modelP);
        }

        public ActionResult ProductDetail(int? Product_ID, string message)
        {
            var modelP = _db.Product.Where(p => p.ID == Product_ID).FirstOrDefault();
            var modelPD = _db.ProductDescrption.Where(p => p.ID == Product_ID).FirstOrDefault();
            var modelPP = _db.ProductPicture.Where(p => p.ID == Product_ID).FirstOrDefault();
            ProductPictureDescrption model = new ProductPictureDescrption();


            model.ProductName = modelP.ProductName;
            model.ProductPrice = modelP.ProductPrice;
            model.ProductQuantity = modelP.ProductQuantity;
            model.ProductDiscount = modelP.ProductDiscount;
            model.ProductCategory = modelPD.ProductCategory;
            model.ProductSubCategory = modelPD.ProductSubCategory;
            model.ProductBrand = modelPD.ProductBrand;
            model.ProductGenderType = modelPD.ProductGenderType;
            model.ProductDescription = modelPD.ProductDescription;
            model.PicturePath = modelPP.PicturePath;
            model.ID = modelP.ID;


            ViewBag.Message = message;
            return View(model);
        }

        [Authorize(Users = "CUSTOMER")]

        public ActionResult AddToKart(long Product_ID)
        {
            var id = Convert.ToInt32(Session["CustomerID"]);

            var modelK = _db.Kart.Where(k => k.Product_ID == Product_ID && k.Customer_ID == id).FirstOrDefault();
            if(modelK != null)
            {
                var modelUpdate = _db.Kart.Where(k => k.ID == modelK.ID).FirstOrDefault();
                modelUpdate.Quantity += 1;

                _db.Entry(modelUpdate).State = EntityState.Modified;
            }
            else
            {
                Kart model = new Kart();
                model.Product_ID = Product_ID;
                model.Customer_ID = id;
                model.Quantity = 1;
                _db.Kart.Add(model);
            }

            _db.SaveChanges();

            var modelP = _db.Product.Where(p => p.ID == Product_ID).FirstOrDefault();

            return RedirectToAction("ProductDetail",new { Product_ID = Product_ID , message = "Product Added To Kart" });
        }

        public ActionResult GoToKart()
        {
            return Content("Inkart");
        }
















        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}