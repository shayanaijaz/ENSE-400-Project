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
    public class Service
    {
        //scalars
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServicePrice { get; set; }
        public float Length { get; set; }

        //references
        public int ServiceTypeID { get; set; }

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