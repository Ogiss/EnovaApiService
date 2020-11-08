using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.BootProcedures
{
    class EnovaBootProcedure : IBootProcedure
    {
        public string Description => "Initialize connection to Enova"; 

        public void OnStart()
        {
            LoadAssemblies();
            Enova.EnovaClient.Connect();
        }

        public void OnStop()
        {
            Enova.EnovaClient.Disconnect();
        }

        private static void LoadAssemblies()
        {
            Soneta.Start.Loader loader = new Soneta.Start.Loader();
            loader.WithExtensions = true;
            loader.Load();
        }
    }
}
