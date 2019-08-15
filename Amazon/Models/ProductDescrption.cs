using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("PRODUCTDESCRPTION")]

    public class ProductDescrption
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string ProductCategory { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string ProductSubCategory { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string ProductBrand { get; set; }

        [Required]
        public string ProductGenderType { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string ProductDescription { get; set; }

        [ForeignKey("Product")]
        public long Product_ID { get; set; }
        public virtual Product Product { get; set; }

    }
}