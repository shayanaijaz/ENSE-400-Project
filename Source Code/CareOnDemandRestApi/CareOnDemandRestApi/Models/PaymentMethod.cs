using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemandRestApi.Models
{
    public class PaymentMethod
    {
        //scalars
        public int PaymentMethodID { get; set; }
        
        //references
        public int CustomerID { get; set; }

        //referred to by
        public ICollection<Order> Orders { get; set; }
    }
}