/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Nicolas Achter
    Contributor(s): 
    Last Modified: Feb. 01, 2020
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemand.Models
{
    public class OrderFor
    {
        //scalars
        public int OrderForID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        //references
        public int CustomerID { get; set; }

        //referred to by
        public ICollection<Order> Orders { get; set; }
    }
}