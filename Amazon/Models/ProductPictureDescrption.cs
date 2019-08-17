using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    public class ProductPictureDescrption
    {
        //product
        [Key]
        public long ID { get; set; }
        

        public string ProductName { get; set; }

        public long ProductPrice { get; set; }

    
        public int ProductQuantity { get; set; }

        public int ProductDiscount { get; set; }

    

        //descrption
        public string ProductCategory { get; set; }
        

        public string ProductSubCategory { get; set; }
        

        public string ProductBrand { get; set; }

        public string ProductGenderType { get; set; }
        

        public string ProductDescription { get; set; }


        //picture
        public string PicturePath { get; set; }
    }
}