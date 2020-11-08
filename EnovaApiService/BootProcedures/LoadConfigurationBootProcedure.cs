using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.BootProcedures
{
    class LoadConfigurationBootProcedure : IBootProcedure
    {
        public string Description => "Loading API configuration";

        public void OnStart()
        {
            Configuration.AppSettings.Load();
        }

        public void OnStop()
        {
            
        }
    }
}
