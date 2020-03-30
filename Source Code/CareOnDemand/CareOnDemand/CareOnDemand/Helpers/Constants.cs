using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.Helpers
{
    class Constants
    {
        public static string AccountsUrl = Settings.UriBase + "/api/Accounts/{0}";
        public static string AccountsEmailUrl = Settings.UriBase + "/api/Accounts/email/{0}";
        public static string AccountLevelsUrl = Settings.UriBase + "/api/AccountLevels/{0}";
        public static string AdminsUrl = Settings.UriBase + "/api/Admins/{0}";
        public static string CarePartnersUrl = Settings.UriBase + "/api/CarePartners/{0}";
        public static string CustomersUrl = Settings.UriBase + "/api/Customers/{0}";
        public static string CustomersAccountIDUrl = Settings.UriBase + "/api/Customers/Account/{0}";
        public static string AddressesUrl = Settings.UriBase + "/api/Addresses/{0}";
        public static string Customer_AddressesUrl = Settings.UriBase + "/api/Customer_Addresses/{0}";
        public static string OrdersUrl = Settings.UriBase + "/api/Orders/{0}";
        public static string OrdersByOrderStatusIDUrl = Settings.UriBase + "/api/Orders/OrderStatus/{0}";
        public static string Order_ServicesUrl = Settings.UriBase + "/api/Order_Services/{0}";
        public static string OrderStatusesUrl = Settings.UriBase + "/api/OrderStatuses/{0}";
        public static string ServiceRequestsUrl = Settings.UriBase + "/api/ServiceRequests/{0}";
        public static string ServiceRequestsByCarePartnerIdUrl = Settings.UriBase + "/api/ServiceRequests/CarePartner/{0}";
        public static string OrderForsUrl = Settings.UriBase + "/api/OrderFors/{0}";
        public static string PaymentMethodsUrl = Settings.UriBase + "/api/PaymentMethods/{0}";
        public static string ServicesUrl = Settings.UriBase + "/api/Services/{0}";
        public static string ServiceTypesUrl = Settings.UriBase + "/api/ServiceTypes/{0}";
    }
}
