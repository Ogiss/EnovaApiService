using Unity;
using EnovaApiService.Framework;
using EnovaApiService.Framework.Logging;
using EnovaApiService.Infrastructure.Logging;
using EnovaApiService.Enova.DiscountGroups.Repositories;

namespace EnovaApiService.Infrastructure
{
    class InfrastructureSetup : IComponentsSetup
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterInstance<ILogger>(LogNames.Api, new Logger(LogNames.Api));
            container.RegisterInstance<ILogger>(LogNames.Performance, new Logger(LogNames.Performance));

            container.RegisterInstance<IDiscountGroupRepository>(new DiscountGroupRepository());

            LogProvider.UnityContainer = container;
            DependencyProvider.UnityContainer = container;
        }
    }
}
