using SuperPayler.Models;
using System.Data.Entity;
using System.Web.Http;

namespace SuperPayler
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new DBContextInitializer());
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
