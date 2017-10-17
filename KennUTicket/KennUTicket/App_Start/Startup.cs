using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Web.Http;

[assembly: OwinStartup(typeof(KennUTicket.App_Start.Startup))]
namespace KennUTicket.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var authenticationOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Users/Login"),
                LogoutPath = new PathString("/Users/Logout"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            };

            app.UseCookieAuthentication(authenticationOptions);

            app.SetDefaultSignInAsAuthenticationType("Application");

        }
    }
}