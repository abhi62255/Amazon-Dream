using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    public class SellerAddress
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Maximun length 200 character")]
        [Display(Name = "Company Name")]
        public string SellerName { get; set; }


        [Required]
        [MaxLength(100, ErrorMessage = "Maximun length 100 character")]
        [Display(Name = "Email")]
        public string SEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minmum length 6 character")]
        [MaxLength(50, ErrorMessage = "Maximun length 50 character")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }





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




    }
}