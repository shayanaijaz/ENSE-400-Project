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
        private Account created_account;
        private Address created_address;

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

            created_account = await accountRestService.SaveAccountAsync(account, true);
        }

        public async Task AddAddress(Address address)
        {
            //Address address = new Address();
            address.AddrLine1 = "121 Solie Crescent";
            address.City = "Regina";
            address.Province = "Saskatchewan";
            address.PostalCode = "S4X3M4";
            
           

            AddressRestService addressRestService = new AddressRestService();

            created_address = await addressRestService.SaveAddressAsync(address, true);

            Customer_Address customer_Address = new Customer_Address();
            customer_Address.CustomerID = created_account.Customer.CustomerID;
            customer_Address.AddressID = created_address.AddressID;
            customer_Address.AddressLabel = "Myself";

            Customer_AddressRestService customer_AddressRestService = new Customer_AddressRestService();
            await customer_AddressRestService.SaveCustomer_AddressAsync(customer_Address, true);

        }
    }
}
