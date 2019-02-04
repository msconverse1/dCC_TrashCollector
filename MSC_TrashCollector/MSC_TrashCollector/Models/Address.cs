using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSC_TrashCollector.Models
{
    public class Address
    {

        [Key]
        public int ID { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [ForeignKey("ZipCode")]
        [Display(Name = "ZipCode")]
        public int ZipCodeID { get; set; }
        public ZipCode ZipCode { get; set; }
    }
}