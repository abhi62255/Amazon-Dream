﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("PRODUCT")]

    public class Product
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(200,ErrorMessage ="Maximum length is 200")]
        public string ProductName { get; set; }

        [Required]
        public long ProductPrice { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

        [Required]
        public int ProductDiscount { get; set; }


        [ForeignKey("Seller")]
        public int Seller_ID { get; set; }
        public virtual Seller Seller { get; set; }


        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
        public virtual ICollection<ProductDescrption> ProductDescrption { get; set; }
        public virtual ICollection<ProductRequest> ProductRequest { get; set; }

    }
}