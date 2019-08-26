using Amazon.Models;
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
        AKARTDBContext _db = new AKARTDBContext();
        // GET: SellerHome
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAndDescription model,string hidden)
        {
            if (ModelState.IsValid)
            {
                hidden = hidden.Remove(hidden.Length - 1, 1) + "}";     //remove , and add } to the description
                model.ProductDescription = hidden;

                Product modelP = new Product();
                ProductDescrption modelD = new ProductDescrption();

                modelP.ProductName = model.ProductName;
                modelP.ProductPrice = model.ProductPrice;
                modelP.ProductQuantity = model.ProductQuantity;
                modelP.ProductDiscount = model.ProductDiscount;
                modelP.Seller_ID = Convert.ToInt32(Session["SellerID"]);
                _db.Product.Add(modelP);
                _db.SaveChanges();

                Session["Product_ID"] = Convert.ToInt32(modelP.ID);

                modelD.Product_ID = modelP.ID;
                modelD.ProductCategory = model.ProductCategory;
                modelD.ProductSubCategory = model.ProductSubCategory;
                modelD.ProductBrand = model.ProductBrand;
                modelD.ProductGenderType = model.ProductGenderType;
                modelD.ProductDescription = model.ProductDescription;
                _db.ProductDescrption.Add(modelD);

                ProductRequest modelPR = new ProductRequest();
                modelPR.Product_ID = modelP.ID;
                _db.ProductRequest.Add(modelPR);


                _db.SaveChanges();

                return RedirectToAction("AddPicture", "SellerHome");
            }

            return View();
        }


        [HttpGet]
        public ActionResult AddPicture()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPicture(ProductPicture model)
        {
            if (ModelState.IsValid)
            {
                model.Product_ID = Convert.ToInt32(Session["Product_ID"]);
                _db.ProductPicture.Add(model);
                _db.SaveChanges();
                return RedirectToAction("AddPicture", "SellerHome");
            }
            return View(model);
        }

    }
}
