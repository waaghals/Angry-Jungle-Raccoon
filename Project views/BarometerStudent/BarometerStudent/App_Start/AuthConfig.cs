using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BarometerStudent;
using BarometerDomain.OAuth;

namespace BarometerStudent
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            OAuthWebSecurity.RegisterGoogleClient();

            OAuthWebSecurity.RegisterClient(new AvansOAuthClient("022dd5cca2fa5cc3b6db3233933f980fc10cb2b4", "f35d8a5e685018bc2208fc0b9eeca5cac0c855c1"), "Avans", null);
        }
    }
}
