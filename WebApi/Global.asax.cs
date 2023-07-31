using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Serilog;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configure Serilog logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
