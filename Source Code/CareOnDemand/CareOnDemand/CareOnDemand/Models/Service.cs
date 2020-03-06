using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemand.Models
{
    public class Service
    {
        //scalars
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServicePrice { get; set; }
        public int Length { get; set; }

        //references
        public int ServiceTypeID { get; set; }
        public ServiceType ServiceType { get; set; }

        //referred to by
        public ICollection<Order_Service> Order_Services { get; set; }
    }

    public class ServiceType
    {
        //scalars
        public int ServiceTypeID { get; set; }
        public string Type { get; set; }

        //referred to by
        public ICollection<Service> Services { get; set; }
    }
}