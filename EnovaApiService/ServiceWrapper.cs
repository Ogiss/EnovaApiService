using System.Reflection;
using System.ServiceProcess;

namespace EnovaApiService
{
    class ServiceWrapper
    {
        private static readonly MethodInfo onStartMethod = typeof(ServiceBase)
            .GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly MethodInfo onStopMethod = typeof(ServiceBase)
            .GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly ServiceBase service;

        public ServiceWrapper(ServiceBase service)
        {
            this.service = service;
        }

        public void Start()
        {
            onStartMethod.Invoke(service, new object[] { new string[] { } });
        }

        public void Stop()
        {
            onStopMethod.Invoke(service, null);
        }
    }
}
