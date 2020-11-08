using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using EnovaApiService.Framework.Logging;

namespace EnovaApiService
{
    class InteractiveApp
    {
        private readonly IUnityContainer unityContainer;
        private readonly ILogger logger;

        public InteractiveApp(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;

            logger = unityContainer.Resolve<ILogger>(LogNames.Api);
        }

        public bool Running { get; set; } = true;

        public void RunInteractiveLoop()
        {
            while (Running)
            {
                Console.Write("> ");

                var line = Console.ReadLine();
                if (line != null)
                {
                    ProcessLineCommand(line);
                }
            }
        }

        private void ProcessLineCommand(string line)
        {
            var elements = line.Split(' ').ToList();
            var command = elements[0];
            var parameters = elements.Skip(1);

            try
            {
                switch (command)
                {
                    case "exit":
                        Running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Warning($"Command execution failed:{ Environment.NewLine }{ e.ToString() }");
            }
        }
    }
}
