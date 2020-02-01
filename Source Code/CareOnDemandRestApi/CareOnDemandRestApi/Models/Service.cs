using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemandRestApi.Models
{
    public class Service
    {
        //scalars
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float ServicePrice { get; set; }

        //reference
        public int ServiceTypeID { get; set; }

        public ICollection<Order_Service> Order_Services { get; set; }
    }

    public class ServiceType
    {
        //scalars
        public int ServiceTypeID { get; set; }
        public string Type { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}