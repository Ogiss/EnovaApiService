using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.BootProcedures
{
    interface IBootProcedure
    {
        string Description { get; }

        void OnStart();
        void OnStop();
    }
}
