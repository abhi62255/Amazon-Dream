using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("PREVISIT")]
    public class PreVisit
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey("Product")]
        public long Product_ID { get; set; }
        public virtual Product Product { get; set; }



        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }

    }
}