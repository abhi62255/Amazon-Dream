using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Amazon.Models
{
    [Table("CITY")]
    public class City
    {
        [Key]
        public int ID { get; set; }
        public string CityName { get; set; }

        [ForeignKey("State")]
        public int State_ID { get; set; }
        public virtual State State { get; set; }
    }
}