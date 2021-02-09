using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace EnovaApiService.BootProcedures
{
    class ApiServiceHostBootProcedure : IBootProcedure
    {
        private HttpSelfHostServer server;
        private Task serverTask;
        public string Description => "Api Service Self Host Server";

        public void OnStart()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:9000");

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", 
                new { controller = "System", id = RouteParameter.Optional });

            server = new HttpSelfHostServer(config);
            serverTask = server.OpenAsync();
            serverTask.Wait();
        }

        public void OnStop()
        {

        }
    }
}
