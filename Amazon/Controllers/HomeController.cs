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

            //making quantity 0 in kart to relese products to sale
            var modelK = _db.Kart.ToList();
            foreach(var kartProduct in modelK)
            {
                var timeDifference = DateTime.Now.Subtract(kartProduct.DateTime).TotalMinutes;
                if(timeDifference >= 120)       //if difference is 2 Hour or more
                {
                    kartProduct.Quantity = 0;
                }
                _db.Entry(kartProduct).State = EntityState.Modified;
            }
            _db.SaveChanges();

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
        [HttpPost]
        public ActionResult ProductDetail(int Product_ID,int Rating, string Review)
        {
            var id = Convert.ToInt32(Session["CustomerID"]);
            var model = new Feedback();
            model.Rating = Rating;
            model.Review = Review;
            model.Customer_ID = id;
            model.Product_ID = Product_ID;
            return RedirectToAction("Create", "ProductFeedback",model);
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
                model.DateTime = DateTime.Now;
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
        public ActionResult Add(int id,int Product_ID)
        {
            var modelK = _db.Kart.Where(k => k.Product_ID == Product_ID).ToList();        //to find availability of product
            var modelP = _db.Product.Where(p => p.ID == Product_ID).FirstOrDefault();
            var AvailableQuantity = modelP.ProductQuantity;

            foreach (var k in modelK)
            {
                AvailableQuantity -= k.Quantity;
            }
            if(AvailableQuantity > 0 )
            {
                var model = _db.Kart.Where(k => k.ID == id).FirstOrDefault();
                model.Quantity += 1;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
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
        public ActionResult Remove(int id)
        {
            var model = _db.Kart.Where(k => k.ID == id).FirstOrDefault();
            _db.Kart.Remove(model);
            _db.SaveChanges();

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
            var Aid = Convert.ToInt32(Session["AddressID"]);
            var modelK = _db.Kart.Where(k => k.Customer_ID == id).ToList();
            var modelA = _db.Address.Where(a => a.ID == Aid).FirstOrDefault();


            //ORDERPLACED TABLE 
            var modelOP = new OrderPlaced();        //populating orderplaed table
            var orderNumber = _db.OrderPlaced.OrderByDescending(o => o.OrderNumber).Select(o => o.OrderNumber).FirstOrDefault();
            if (orderNumber == 0)
            {
                orderNumber = 999;
            }
            orderNumber += 1;

            modelOP.OrderNumber = orderNumber;

            modelOP.Address = "Address Line: " + modelA.AddressLine1 + ", City: " + modelA.City + ", State: " + modelA.State +
                ", PostalCode: " + modelA.PostalCode + ", Address Type: " + modelA.AddressType;

            modelOP.DateTime = DateTime.Now;
            modelOP.PaymentType = Session["PaymentType"].ToString();
            modelOP.Status = "Placed will be with you soon";
            modelOP.Customer_ID = id;

            


            foreach (var item in modelK)     //checking availability a
            {
                var modelP = _db.Product.Where(p => p.ID == item.Product_ID).FirstOrDefault();
                if(modelP.ProductQuantity < item.Quantity)
                {
                    return Content("Products Out of Stock Try again later : " + modelP.ProductName);
                }
                modelP.ProductQuantity -= item.Quantity;

                modelOP.Quantity = item.Quantity;
                var ActualPrice = item.Product.ProductPrice - (item.Product.ProductPrice * item.Product.ProductDiscount) / 100;
                modelOP.Amount = item.Quantity * ActualPrice;
                modelOP.Product_ID = item.Product_ID;

                _db.OrderPlaced.Add(modelOP);
                _db.SaveChanges();
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