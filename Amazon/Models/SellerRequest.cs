using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("SELLERREQUEST")]
    public class SellerRequest
    {
        public int ID { get; set; }

        [ForeignKey("Seller")]
        public int Seller_ID { get; set; }
        public virtual Seller Seller { get; set; }


    }
}