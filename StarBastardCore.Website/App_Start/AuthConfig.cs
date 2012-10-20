using Microsoft.Web.WebPages.OAuth;
using StarBastardCore.Website.Code;
using StarBastardCore.Website.Filters;

namespace StarBastardCore.Website.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            var settings = new AppSettingsWrapper();
            // For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            //OAuthWebSecurity.RegisterMicrosoftClient(clientId: "", clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(consumerKey: settings.Get<string>("Twitter.AppId"),
                                                    consumerSecret: settings.Get<string>("Twitter.AppSecret"));

            OAuthWebSecurity.RegisterFacebookClient(appId: settings.Get<string>("Facebook.AppId"),
                                                    appSecret: settings.Get<string>("Facebook.AppSecret"));

            OAuthWebSecurity.RegisterGoogleClient();
        
            InitializeSimpleMembershipAttribute.SimpleMembershipInitializer.Init();
        }
    }
}
