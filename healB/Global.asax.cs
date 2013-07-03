using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace healB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeAuthenticationConnection();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private void InitializeAuthenticationConnection()
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var simpleMembershipConnectionString = configuration.ConnectionStrings.ConnectionStrings["SimpleMembershipConnectionString"];

            if (simpleMembershipConnectionString == null)
            {
                var connectionString = new SqlConnectionStringBuilder();

                //sqlserver://username:password@hostname/databasename
                var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
                if (uriString == null)
                {
                    // Local
                    connectionString.DataSource = "DH";
                    connectionString.InitialCatalog = "healB";
                    connectionString.IntegratedSecurity = true;
                }
                else
                {
                    // Cloud
                    var uri = new Uri(uriString);
                    connectionString.DataSource = uri.Host;
                    connectionString.InitialCatalog = uri.AbsolutePath.Trim('/');
                    connectionString.UserID = uri.UserInfo.Split(':').First();
                    connectionString.Password = uri.UserInfo.Split(':').Last();
                }

                simpleMembershipConnectionString = new ConnectionStringSettings()
                {
                    Name = "SimpleMembershipConnectionString",
                    ConnectionString = connectionString.ConnectionString,
                    ProviderName = "System.Data.SqlClient"
                };
                configuration.ConnectionStrings.ConnectionStrings.Add(simpleMembershipConnectionString);
                configuration.Save();
            }
        }
    }
}
