using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareOnDemand.Models
{
    public class Account
    {
        //scalars
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        //references
        public AccountLevel AccountLevel { get; set; }

        public ICollection<Admin> Admins { get; set; }
        public ICollection<CarePartner> CarePartners { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }

    public class AccountLevel
    {
        //scalars
        public int AccountLevelID { get; set; }
        public string LevelTitle { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }

    public class Admin
    {
        //scalar
        public int AdminID { get; set; }

        //reference
        public Account Account { get; set; }
    }

    public class CarePartner
    {
        //scalars
        public int CarePartnerID { get; set; }
        public string Company { get; set; }

        //reference
        public Account Account { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }

    public class Customer
    {
        //scalar
        public int CustomerID { get; set; }

        //reference
        public Account Account { get; set; }

        public ICollection<Customer_Address> Customer_Addresses { get; set; }
        public ICollection<OrderFor> OrderFors { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}