/*
    Care on Demand Application
    Capstone 2020 - ENSE 400/477
    The Ni(c)(k)S

    Author: Shayan Khan
    Last Modified: Apr. 07, 2020
*/
using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using System.Threading.Tasks;
using CareOnDemand.Data;

namespace CareOnDemand.Models
{
    /*
     * This class contains functions related to logging in a user. It uses the Amazon Cognito packages to 
     * log in a user and also uses the REST service to interact with the database and retrieve a user
     */
    public class LoginService
    {
        private string username;
        private string password;
        public LoginService(string userEmail, string userPassword)
        {
            username = userEmail;
            password = userPassword;
        }

        // Log in a user using credential provided. Initializes a secure connection to AWS and logs in the user.
        public async Task Login()
        {
            AmazonCognitoIdentityProviderClient provider =
                    new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);
            CognitoUserPool userPool = new CognitoUserPool(Settings.AWS_COGNITO_POOL_ID, Settings.AWS_CLIENT_ID, provider);
            CognitoUser user = new CognitoUser(username, Settings.AWS_CLIENT_ID, userPool, provider);
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };

            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
        }

        // Function that uses the REST service to retrieve a user from the database.
        public async Task<Account> GetUserFromDatabase()
        {
            AccountRestService accountRestService = new AccountRestService();

            var result = await accountRestService.GetAccountByEmailAsync(username);

            return result[0];
        }
    }
}
