using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("SELLER")]
    public class Seller
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Maximun length 200 character")]
        [Display(Name = "Company Name")]
        public string SellerName { get; set; }


        [Required]
        [MaxLength(100, ErrorMessage = "Maximun length 100 character")]
        [Display(Name ="Email")]
        [EmailAddress]
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


        public virtual ICollection<Address> Address { get; set; }


    }
}