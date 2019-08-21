using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("CUSTOMER")]
    public class Customer
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Maximun length 70 character")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(70, ErrorMessage = "Maximun length 70 character")]
        public string Email { get; set; }

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
        public string Gender { get; set; }


        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Kart> Kart { get; set; }
        public virtual ICollection<OrderPlaced> OrderPlaced { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }




    }
}