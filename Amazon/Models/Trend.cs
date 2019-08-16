using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("TREND")]
    public class Trend
    {
        public int ID { get; set; }
        
        [ForeignKey("Product")]
        public long Product_ID { get; set; }
        public virtual Product Product { get; set; }
    }
}