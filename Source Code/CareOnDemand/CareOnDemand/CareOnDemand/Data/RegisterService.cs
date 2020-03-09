using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
//using Amazon.CognitoIdentity;
//using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using CareOnDemand.Models;
using CareOnDemand.Data;

namespace CareOnDemand.Models
{
    public class RegisterService
    {
        private String email;
        private String password;
        private String number;
        private String name;
        private String address_line_1;
        private String address_city;
        private String address_province;
        private String address_postal_code;

        public RegisterService(Account account, Address customer_address)
        {
            email = account.Email;
            password = account.Password;
            number = account.PhoneNumber;
            name = account.FirstName + " " + account.LastName;
            address_line_1 = customer_address.AddrLine1;
            address_city = customer_address.City;
            address_province = customer_address.Province;
            address_postal_code = customer_address.PostalCode;
        }

        public async Task CreateCognitoUser()
        {
            AmazonCognitoIdentityProviderClient client =
                     new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);


            var signUpRequest = new SignUpRequest
            {
                ClientId = Settings.AWS_CLIENT_ID,
                Password = password,
                Username = email,
            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = email
            };

            var nameAttribute = new AttributeType
            {
                Name = "name",
                Value = name
            };

            var numberAttribute = new AttributeType
            {
                Name = "phone_number",
                Value = number
            };

            signUpRequest.UserAttributes.Add(emailAttribute);
            signUpRequest.UserAttributes.Add(nameAttribute);
            signUpRequest.UserAttributes.Add(numberAttribute);

            var result = await client.SignUpAsync(signUpRequest);

            Console.Out.WriteLine(result);



        }

        public async Task CreateDatabaseUser(Account account)
        {
            AccountRestService accountRestService = new AccountRestService();

            AccountLevelRestService accountLevelRestService = new AccountLevelRestService();

            var accountLevel = await accountLevelRestService.RefreshDataAsync();

            foreach (var level in accountLevel)
            {
                if (level.LevelTitle.Trim() == "Customer")
                    account.AccountLevelID = level.AccountLevelID;
            }


            await accountRestService.SaveAccountAsync(account, true);
            var result = await accountRestService.RefreshDataAsync();
        }

        public async Task AddAddress(Account account)
        {
            Address address = new Address();
            address.AddrLine1 = "111 Solie Crescent";
            address.City = "Regina";
            address.Province = "Saskatchewan";
            address.PostalCode = "S4X3M4";

            

            AddressRestService addressRestService = new AddressRestService();
            //Customer_AddressRestService customer_AddressRestService = new Customer_AddressRestService();

            //Customer_Address customer_Address = new Customer_Address();
            //customer_Address.AddressLabel = "Home";

            ////account.Customer.Customer_Addresses = new List<Customer_Address>();

            //address.Customer_Addresses = new List<Customer_Address>();
            //address.Customer_Addresses.Add(customer_Address);

            //account.Customer.Customer_Addresses = new List<Customer_Address>();
            //account.Customer.Customer_Addresses.Add(customer_Address);

            var created_address = await addressRestService.SaveAddressAsync(address, true);
            var result = await addressRestService.RefreshDataAsync();

            //await customer_AddressRestService.SaveCustomer_AddressAsync(customer_Address, true);
            //var result2 = await customer_AddressRestService.RefreshDataAsync();

            


        }
    }
}
