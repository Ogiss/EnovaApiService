using Unity;
using EnovaApiService.Framework;
using EnovaApiService.Framework.Logging;
using EnovaApiService.Infrastructure.Logging;

namespace EnovaApiService.Infrastructure
{
    class InfrastructureSetup : IComponentsSetup
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterInstance<ILogger>(LogNames.Api, new Logger(LogNames.Api));
            container.RegisterInstance<ILogger>(LogNames.Performance, new Logger(LogNames.Performance));

            LogProvider.UnityContainer = container;
            DependencyProvider.UnityContainer = container;
        }
    }
}
