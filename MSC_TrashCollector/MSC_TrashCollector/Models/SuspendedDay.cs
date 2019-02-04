using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSC_TrashCollector.Models
{
    public class SuspendedDay
    {
        [Key]
        public int ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}