using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Unity;

namespace EnovaApiService.BootProcedures
{
    class AutoMapperBootProcedure : IBootProcedure
    {
        private readonly IUnityContainer container;
        private MapperConfiguration config = null;
        private IMapper mapper = null;

        public string Description => "Initializing Automapper";

        public AutoMapperBootProcedure(IUnityContainer container)
        {
            this.container = container;
        }

        public void OnStart()
        {
            config = new MapperConfiguration(cfg =>
            {
                AutoMapper.AutoMapperConfig.ConfigureMappings(cfg);
            });

            mapper = config.CreateMapper();

            container.RegisterInstance(mapper);
        }

        public void OnStop()
        {
        }
    }
}
