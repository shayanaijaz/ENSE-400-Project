using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace CareOnDemandRestApi.Models
{
    public class CareOnDemandContext : DbContext
    {
        public CareOnDemandContext(DbContextOptions<CareOnDemandContext> options) : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountLevel> AccountLevels { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CarePartner> CarePartners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer_Address> Customer_Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Service> Order_Services { get; set; }
        public DbSet<OrderFor> OrderFors { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
    }
}