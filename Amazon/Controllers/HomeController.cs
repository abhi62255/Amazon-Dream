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
        [Authorize]
        public ActionResult GoToKart()
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var modelK = _db.Kart.Where(k => k.Customer_ID == id).ToList();

            return View(modelK);
        }
        public ActionResult Add(int id)
        {
            var model = _db.Kart.Where(k => k.ID == id).FirstOrDefault();
            model.Quantity += 1;
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("GoToKart");

        }
        public ActionResult Subtract(int id)
        {
            var model = _db.Kart.Where(k => k.ID == id).FirstOrDefault();
            if(model.Quantity != 0)
            {
                model.Quantity -= 1;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            
            return RedirectToAction("GoToKart");
        }
        public ActionResult AddAddress()
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var model = _db.Address.Where(a => a.Customer_ID == id).ToList();


            return View(model);
        }

        [HttpGet]
        public ActionResult NewAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAddress(Address model)
        {
            model.Customer_ID = Convert.ToInt32(Session["CustomerID"]);
            _db.Address.Add(model);
            _db.SaveChanges();

            return RedirectToAction("AddAddress");
        }

        public ActionResult DeleteAddress(int id)
        {
            var model = _db.Address.Find(id);
            _db.Address.Remove(model);
            _db.SaveChanges();

            return RedirectToAction("AddAddress");
        }

        public ActionResult SelectAddress(int id)
        {
            Session["AddressID"] = id;

            return RedirectToAction("AddPayment");
        }


        [HttpGet]
        public ActionResult AddPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPayment(string PaymentType)
        {
            Session["PaymentType"] = PaymentType;

            return RedirectToAction("VerifyOrder");
        }

        public ActionResult VerifyOrder()
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var modelK = _db.Kart.Where(k => k.Customer_ID == id).ToList();
            return View(modelK);
        }

        public ActionResult PlaceOrder()
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var modelK = _db.Kart.Where(k => k.Customer_ID == id).ToList();

            foreach(var item in modelK)
            {
                var modelP = _db.Product.Where(p => p.ID == item.Product_ID).FirstOrDefault();
                if(modelP.ProductQuantity < item.Quantity)
                {
                    return Content("Products Out of Stock Try again later : " + modelP.ProductName);
                }
                modelP.ProductQuantity -= item.Quantity;
                _db.Entry(modelP).State = EntityState.Modified;
                _db.Kart.Remove(item);      //remove items from kart after placing order
            }
            _db.SaveChanges();

            return Content("OrderPlaced");
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