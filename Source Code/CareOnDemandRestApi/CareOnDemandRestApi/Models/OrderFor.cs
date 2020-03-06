using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemandRestApi.Models
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