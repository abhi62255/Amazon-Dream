using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("SEARCHHISTORY")]
    public class SearchHistory
    {
        [Key]
        public int ID { get; set; }
        public string SearchTag { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}