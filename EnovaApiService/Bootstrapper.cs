using Unity;
using Unity.Interception;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using EnovaApiService.Framework;

namespace EnovaApiService
{
    class Bootstrapper
    {
        private static readonly Type[] componentSetups = new Type[]
        {
            typeof(Infrastructure.InfrastructureSetup),
        };

        private InteractiveApp interactiveApp;

        public UnityContainer UnityContainer { get; }

        public Bootstrapper()
        {
            // For exceptions to be localized in English
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");

            UnityContainer = new UnityContainer();
            UnityContainer.AddNewExtension<Interception>();
        }

        public void ConfigureComponents()
        {
            foreach (var setup in componentSetups)
            {
                var configurator = (IComponentsSetup)Activator.CreateInstance(setup);
                configurator.RegisterComponents(UnityContainer);
            }
        }

        public void RunServices()
        {
            var services = new ServiceBase[]
            {
                new CoreService(UnityContainer)
            };

            if (Environment.UserInteractive)
            {
                RunAsConsoleApp(services, UnityContainer);
            }
            else
            {
                RunAsWindowsService(services);
            }
        }

        private void RunAsWindowsService(ServiceBase[] services)
        {
            ServiceBase.Run(services);
        }

        private void RunAsConsoleApp(ServiceBase[] services, UnityContainer unityContainer)
        {
            Console.WriteLine("[ Enova API ]");
            Console.WriteLine();

            var serviceWrappers = services
                .Select(x => new ServiceWrapper(x))
                .ToList();

            try
            {
                serviceWrappers.ForEach(svc => svc.Start());

                Console.WriteLine();
                Console.WriteLine("Application started.");
                Console.WriteLine();

                interactiveApp = new InteractiveApp(unityContainer);
                interactiveApp.RunInteractiveLoop();

                serviceWrappers.ForEach(svc => svc.Stop());

                Console.WriteLine();
                Console.WriteLine("Shutting down ...");

                Thread.Sleep(1000);
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}
