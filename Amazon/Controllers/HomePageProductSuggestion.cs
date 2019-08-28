using Amazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazon.Controllers
{
    public class HomePageProductSuggestion
    {
        AKARTDBContext _db = new AKARTDBContext();
       public List<Product> ProductSuggestion()
       {
            var modelSP = new List<Product>();
            if (System.Web.HttpContext.Current.Session["CustomerID"] != null ) 
            {
                var id = Convert.ToInt32(System.Web.HttpContext.Current.Session["CustomerID"]);

                var modelWL = _db.Wishlist.Where(c => c.Customer_ID == id).Select(c => c.Product.ProductDescrption).ToList();
                var modelPV = _db.PreVisit.Where(c => c.Customer_ID == id).Select(c => c.Product.ProductDescrption).ToList();
                var modelK = _db.Kart.Where(c => c.Customer_ID == id).Select(c => c.Product.ProductDescrption).ToList();


                foreach(var pro in modelWL)         //sugesting product from Wishlist
                {
                    foreach(var i in pro)
                    {
                        var model = _db.ProductDescrption.Where(p => p.ProductSubCategory == i.ProductSubCategory).Select(p=>p.Product).ToList();
                        foreach(var j in model)
                        {
                            if (!modelSP.Contains(j))
                            {
                                modelSP.Add(j);
                            }
                        }
                    }
                }
                foreach (var pro in modelPV)         //sugesting product from Previsit
                {
                    foreach (var i in pro)
                    {
                        var model = _db.ProductDescrption.Where(p => p.ProductSubCategory == i.ProductSubCategory).Select(p => p.Product).ToList();
                        foreach (var j in model)
                        {
                            if (!modelSP.Contains(j))
                            {
                                modelSP.Add(j);
                            }
                        }
                    }
                }
                foreach (var pro in modelK)         //sugesting product from Kart
                {
                    foreach (var i in pro)
                    {
                        var model = _db.ProductDescrption.Where(p => p.ProductSubCategory == i.ProductSubCategory).Select(p => p.Product).ToList();
                        foreach (var j in model)
                        {
                            if (!modelSP.Contains(j))
                            {
                                modelSP.Add(j);
                            }
                        }
                    }
                }
            }
                return modelSP;
       }
    }
}