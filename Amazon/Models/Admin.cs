using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("ADMIN")]
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }

    }
}