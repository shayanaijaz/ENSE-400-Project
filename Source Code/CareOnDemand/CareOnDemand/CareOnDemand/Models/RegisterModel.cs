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

namespace CareOnDemand.Models
{
    class RegisterModel
    {
        private String email;
        private String password;
        private String number;
        private String name;
        private String address;

        public RegisterModel(String userEmail, String userPassword, String userNumber, String userFullName, String userAddress)
        {
            email = userEmail;
            password = userPassword;
            number = userNumber;
            name = userFullName;
            address = userAddress;
        }

        public async void CreateCognitoUser()
        {

            //var client = new AmazonCognitoIdentityProviderClient();
            AmazonCognitoIdentityProviderClient client =
                     new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);

            //var userPool = new CognitoUserPool(Settings.AWS_COGNITO_POOL_ID, Settings.AWS_CLIENT_ID, client);



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
