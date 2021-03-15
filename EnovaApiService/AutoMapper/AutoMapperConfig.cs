using AutoMapper;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        public static void ConfigureMappings(IMapperConfigurationExpression cfg)
        {
            Common.ConfigureMappings(cfg);
            Address.ConfigureMappings(cfg);
            Customer.ConfigureMappings(cfg);
            CommercialDocument.ConfigureMappings(cfg);
            Product.ConfigureMappings(cfg);
        }
    }
}
