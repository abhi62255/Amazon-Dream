using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("ADDRESS")]
    public class Address
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Maximun length 500 character")]
        public string AddressLine1 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public Int32 PostalCode { get; set; }

        public string AddressType { get; set; }

        [ForeignKey("Seller")]
        public Nullable<int> Seller_ID { get; set; }
        public virtual Seller Seller { get; set; }

        [ForeignKey("Customer")]
        public Nullable<int> Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }


    }
}