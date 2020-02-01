using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CareOnDemandRestApi.Models
{
    public class Order
    {
        //scalars
        public int OrderID { get; set; }
        public string OrderInstructions { get; set; }

        //references
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public int OrderForID { get; set; }
        public int PaymentMethodID { get; set; }
        public int OrderStatusID { get; set; }

        public ICollection<Order_Service> Order_Services { get; set; }
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }

    public class Order_Service
    {
        //references
        [Key]
        public int OrderID { get; set; }
        public int ServiceID { get; set; }
    }

    public class OrderStatus
    {
        //scalars
        public int OrderStatusID { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class ServiceRequest
    {
        //references
        [Key]
        public int OrderID { get; set; }
        public int CarePartnerID { get; set; }
    }
}