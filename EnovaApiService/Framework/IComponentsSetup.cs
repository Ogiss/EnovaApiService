using Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.Framework
{
    public interface IComponentsSetup
    {
        void RegisterComponents(UnityContainer container);
    }
}
