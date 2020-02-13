using System;
using System.Collections.Generic;
using System.Text;
using CareOnDemand.Helpers;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System.Threading.Tasks;

namespace CareOnDemand.Data
{
    public class ForgotPasswordService
    {
        private String email;

        public ForgotPasswordService(String userEmail)
        {
            email = userEmail;
        }

        public async Task ForgotPassowrd()
        {
            AmazonCognitoIdentityProviderClient provider =
                    new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);
            CognitoUserPool userPool = new CognitoUserPool(Settings.AWS_COGNITO_POOL_ID, Settings.AWS_CLIENT_ID, provider);
            CognitoUser user = new CognitoUser(email, Settings.AWS_CLIENT_ID, userPool, provider);

            await user.ForgotPasswordAsync();
        }

        public async Task ForgotPasswordConfirmation()
        {

        }


    }
}
