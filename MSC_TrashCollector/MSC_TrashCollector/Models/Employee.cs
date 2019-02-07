using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSC_TrashCollector.Models
{
    public class Employee
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
        [Display(Name = "PickupAddress")]
        public int AddressID { get; set; }
        public Address Address { get; set; }

        public string ANUserID { get; set; }
        public IEnumerable<int> ZipCodeSelect { get; set; }
    }
}