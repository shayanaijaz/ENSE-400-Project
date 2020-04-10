/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Contributor(s): Nicolas Achter
    Last Modified: Apr. 07, 2020
*/
using System;
using System.Threading.Tasks;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CareOnDemand.Data;

namespace CareOnDemand.Models
{
    /*
     * This class defines functions relating to registering a new user. The user is registered in AWS and their details
     * are added to the database.
     */
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

        public RegisterService(Account account)
        {
            email = account.Email;
            password = account.Password;
            number = account.PhoneNumber;
            name = account.FirstName + " " + account.LastName;
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

        public async Task CreateDatabaseUser(Account account, int accountLevelID)
        {
            AccountRestService accountRestService = new AccountRestService();
            account.AccountLevelID = accountLevelID;
            created_account = await accountRestService.SaveAccountAsync(account, true);
        }

        public async Task AddAddress(Address address)
        {
            AddressRestService addressRestService = new AddressRestService();

            created_address = await addressRestService.SaveAddressAsync(address, true);

            Customer_Address customer_Address = new Customer_Address();
            customer_Address.CustomerID = created_account.Customer.CustomerID;
            customer_Address.AddressID = created_address.AddressID;
            customer_Address.AddressLabel = "Default";

            Customer_AddressRestService customer_AddressRestService = new Customer_AddressRestService();
            await customer_AddressRestService.SaveCustomer_AddressAsync(customer_Address, true);

        }
    }
}
