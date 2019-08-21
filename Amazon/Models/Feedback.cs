using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("FEEDBACK")]
    public class Feedback
    {
        [Key]
        public long ID { get; set; }

        [Range(1,5)]
        [Required]
        public int  Rating { get; set; }

        [Required]
        [MaxLength(5000,ErrorMessage ="Maximum length of 5000 Character")]
        public string Review { get; set; }


        [ForeignKey("Product")]
        public long Product_ID { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }


    }
}