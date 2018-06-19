using SOASuite.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOASuite.Models {
    public class OrderModel {

        public string City { get; set; }
        public string AddressLine { get; set; }
        public string[] Address { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }

        public string Ordernumber { get; set; }

        public ItemType[] Items { get; set; }

    }
}