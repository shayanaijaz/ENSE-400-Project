using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemand.Models
{
    public class PaymentMethod
    {
        //scalars
        public int PaymentMethodID { get; set; }
        
        //references
        public int CustomerID { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}