using AutoMapper;
using EnovaApi.Models.Common;
using Soneta.Business;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        private static class Common
        {
            public static void ConfigureMappings(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<Row, Entity>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.ID));

                cfg.CreateMap<GuidedRow, GuidedEntity>()
                    .IncludeBase<Row, Entity>()
                    .ForMember(d => d.Guid, o => o.MapFrom(s => s.Guid));
            }
        }
    }
}
