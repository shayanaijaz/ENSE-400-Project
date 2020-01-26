using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareOnDemand.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        public static string UrlBase
        {
            get => AppSettings.GetValueOrDefault(nameof(UrlBase), "TEST_URL");
            set => AppSettings.AddOrUpdateValue(nameof(UrlBase), value);
        }

        public static string AWS_REGION
        {
            get => AppSettings.GetValueOrDefault(nameof(AWS_REGION), "ca-central-1");
            set => AppSettings.GetValueOrDefault(nameof(AWS_REGION), value);
        }

        public static string AWS_CLIENT_ID
        {
            get => AppSettings.GetValueOrDefault(nameof(AWS_CLIENT_ID), "3347gn412kvgl1gtus49go3tvo");
            set => AppSettings.GetValueOrDefault(nameof(AWS_CLIENT_ID), value);
        }

        public static string AWS_COGNITO_POOL_ID
        {
            get => AppSettings.GetValueOrDefault(nameof(AWS_COGNITO_POOL_ID), "ca-central-1_5g4GbAmLU");
            set => AppSettings.GetValueOrDefault(nameof(AWS_COGNITO_POOL_ID), value);
        }









    }
}
