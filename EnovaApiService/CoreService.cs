using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;
using EnovaApiService.Framework.Logging;
using EnovaApiService.BootProcedures;


namespace EnovaApiService
{
    partial class CoreService : ServiceBase
    {
        private IUnityContainer unityContainer;
        private readonly IEnumerable<IBootProcedure> bootProcedures;
        private readonly ILogger logger = LogProvider.GetLogger(LogNames.Api);

        public CoreService(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            InitializeComponent();

            bootProcedures = new IBootProcedure[]
            {
                new LoadConfigurationBootProcedure(),
                new EnovaBootProcedure(),
                new AutoMapperBootProcedure(unityContainer),
                new ApiServiceHostBootProcedure()
            };
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                foreach(var proc in bootProcedures)
                {
                    logger.Info("Booting: " + proc.Description);

                    try
                    {
                        proc.OnStart();
                    }
                    catch(BootProcedureException e)
                    {
                        if (!e.IsCritical)
                        {
                            logger.Warning("Boot procedure execution failed: " + e.ToString());
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

        protected override void OnStop()
        {
            foreach(var proc in bootProcedures)
            {
                proc.OnStop();
            }
        }
    }
}
