using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSC_TrashCollector.Models
{
    public class ViewModel
    {
        public Customer Customer { get; set; }
        public Address Address { get; set; }

        public SuspendedDay SuspendedDay { get; set; }
        public ExtraPickupDate ExtraPickupDate { get; set; }
    }
}