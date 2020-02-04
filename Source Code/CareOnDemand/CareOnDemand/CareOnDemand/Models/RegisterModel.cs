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
using CareOnDemandRest.Models;

namespace CareOnDemand.Models
{
    public class RegisterModel
    {
        private String email;
        private String password;
        private String number;
        private String name;
        private String address_line_1;
        private String address_city;
        private String address_province;
        private String address_postal_code;

        public RegisterModel(Customer customer, Address customer_address)
        {
            email = customer.Account.Email;
            password = customer.Account.Password;
            number = customer.Account.PhoneNumber;
            name = customer.Account.FirstName + " " + customer.Account.LastName;
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
    }
}
