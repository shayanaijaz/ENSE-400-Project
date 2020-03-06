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
        public string PasswordConfirmation { get; set; }

        //references
        public int AccountLevelID { get; set; }

        //referred to by
        public Admin Admin { get; set; }
        public CarePartner CarePartner { get; set; }
        public Customer Customer { get; set; }
    }

    public class AccountLevel
    {
        //scalars
        public int AccountLevelID { get; set; }
        public string LevelTitle { get; set; }

        //referred to by
        public ICollection<Account> Accounts { get; set; }
    }

    public class Admin
    {
        //scalars
        public int AdminID { get; set; }

        //references
        public int AccountID { get; set; }
    }

    public class CarePartner
    {
        //scalars
        public int CarePartnerID { get; set; }
        public string Company { get; set; }

        //references
        public int AccountID { get; set; }

        //referred to by
        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }

    public class Customer
    {
        //scalars
        public int CustomerID { get; set; }

        //references
        public int AccountID { get; set; }

        //referred to by
        public ICollection<Customer_Address> Customer_Addresses { get; set; }
        public ICollection<OrderFor> OrderFors { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}