using Amazon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Amazon.Controllers
{
    public class RegisterLoginController : Controller
    {
        AKARTDBContext _db = new AKARTDBContext();

        // GET: RegistraionLogin
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Hashing.Hash(model.Password);
                model.ConfirmPassword = Hashing.Hash(model.ConfirmPassword);

                var modelS = _db.Seller.Where(s => s.SEmail == model.Email).FirstOrDefault();
                var modelA = _db.Admin.Where(s => s.Email == model.Email).FirstOrDefault();
                var modelC = _db.Customer.Where(s => s.Email == model.Email).FirstOrDefault();

                if (modelA != null || modelC != null)
                {
                    ViewBag.message = "Email Address Is Already Registered";
                    return View();
                }
               if(modelS != null)
                {
                    ViewBag.message = "Email Address Is Already Registered as Seller";
                    return View();
                }
                
                _db.Customer.Add(model);
                _db.SaveChanges();
                FormsAuthentication.SetAuthCookie(model.Name, false);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Customer model)
        {
            model.Password = Hashing.Hash(model.Password);
            var admin = _db.Admin.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();
            if(admin == null)
            {
                var seller = _db.Seller.Where(a => a.SEmail.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();
                if(seller == null)
                {
                    var user = _db.Customer.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password)).FirstOrDefault();

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Name, false);
                        Session["CustomerID"] = user.ID;
                        Session["Identity"] = "CUSTOMER";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.message = "Wrong User Credentials";
                        return View();
                    }
                }
                else
                {
                    Session["SellerID"] = seller.ID;
                    Session["Identity"] = "SELLER";
                    var id = Convert.ToInt32(Session["SellerID"]);
                    var value = _db.SellerRequest.Where(s => s.Seller_ID == id).FirstOrDefault();
                    if (value != null)
                    {

                        return Content("Your profile is still not verified");
                    }
                    FormsAuthentication.SetAuthCookie("SELLER", false);
                    return RedirectToAction("Index", "SellerHome");
                }
            }
            else
            {
                FormsAuthentication.SetAuthCookie("ADMIN", false);
                Session["SellerID"] = admin.ID;
                Session["Identity"] = "ADMIN";
                return RedirectToAction("Index", "AdminHome");
            }
            
        }


        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult SellerRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SellerRegistration(SellerAddress model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Hashing.Hash(model.Password);
                model.ConfirmPassword = Hashing.Hash(model.ConfirmPassword);
                Seller modelS = new Seller();
                Address modelA = new Address();

                modelS.SellerName = model.SellerName;
                modelS.SEmail = model.SEmail;
                modelS.Password = model.Password;
                modelS.ConfirmPassword = model.ConfirmPassword;

                modelA.AddressLine1 = model.AddressLine1;
                modelA.City = model.City;
                modelA.State = model.State;
                modelA.PostalCode = model.PostalCode;
                modelA.AddressType = model.AddressType;


                _db.Seller.Add(modelS);
                _db.SaveChanges();

                modelA.Seller_ID = modelS.ID;
                _db.Address.Add(modelA);
                _db.SaveChanges();


                var modelSR = new SellerRequest();
                modelSR.Seller_ID = modelS.ID;
                _db.SellerRequest.Add(modelSR);
                _db.SaveChanges();

                return RedirectToAction("LogIn", "RegisterLogin");
            }
            return View(model);

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