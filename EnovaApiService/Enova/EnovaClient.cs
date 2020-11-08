using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Business;
using Soneta.Business.App;
using EnovaApiService.Configuration;

namespace EnovaApiService.Enova
{
    partial class EnovaClient
    {
        public static Database Database { get; private set; }
        public static Login Login { get; private set; }
        public static void Connect()
        {
            Database = BusApplication.Instance[AppSettings.EnovaDatabaseName];
            Login = Database.Login(false, AppSettings.EnovaLogin, AppSettings.EnovaPassword ?? "");
        }

        public static void Disconnect()
        {
            if(EnovaClient.Login != null)
            {
                EnovaClient.Login.Dispose();
                EnovaClient.Login = null;
            }
        }
    }
}
