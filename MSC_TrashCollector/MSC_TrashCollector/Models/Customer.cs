using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSC_TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ForeignKey("Address")]
        [Display(Name = "Address")]
        public int AddressID { get; set; }
        public Address Address { get; set; }

        [ForeignKey("SuspendedDay")]
        [Display(Name = "SuspendedDay")]
        public int? SuspendedDayId { get; set; }
        public SuspendedDay SuspendedDay { get; set; }
        
        public bool TrashPickup { get; set; }
        public string ANUserID { get; set;}
       
        public double PickupCost { get; set; }

        public string key { get; set; }

    }
}