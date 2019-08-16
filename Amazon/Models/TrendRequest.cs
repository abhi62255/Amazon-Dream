using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("TRENDREQUEST")]
    public class TrendRequest
    {
        public int ID { get; set; }

        [ForeignKey("Product")]
        public long Product_ID { get; set; }
        public virtual Product Product { get; set; }
    }
}