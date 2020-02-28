using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareOnDemand.Data
{
    public class ForgotPasswordService
    {
        private String email;
        private AmazonCognitoIdentityProviderClient provider;
        private CognitoUserPool userPool;
        private CognitoUser user;

        public ForgotPasswordService(String userEmail)
        {
            email = userEmail;
            provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);
            userPool = new CognitoUserPool(Settings.AWS_COGNITO_POOL_ID, Settings.AWS_CLIENT_ID, provider);
            user = new CognitoUser(email, Settings.AWS_CLIENT_ID, userPool, provider);
        }

        public async Task ForgotPassowrd()
        {
            await user.ForgotPasswordAsync();
        }

        public async Task ForgotPasswordConfirmation(String verificationCode, String newPassword)
        {
            await user.ConfirmForgotPasswordAsync(verificationCode, newPassword);
        }


    }
}
