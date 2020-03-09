using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareOnDemandRestApi.Models
{
    public class Address
    {
        //scalars
        public int AddressID { get; set; }
        public string AddrLine1 { get; set; }
        public string AddrLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

        //referred to by
        public ICollection<Customer_Address> Customer_Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class Customer_Address
    {
        //scalars
        public string AddressLabel { get; set; }

        //references
        [Key]
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public int CustomerID { get; set; }
    }
}