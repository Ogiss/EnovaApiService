using Unity;

namespace EnovaApiService.Framework.Logging
{
    public class LogProvider
    {
        public static IUnityContainer UnityContainer { get; set; }

        public static ILogger GetLogger(string name)
        {
            return UnityContainer.Resolve<ILogger>(name);
        }
    }
}
