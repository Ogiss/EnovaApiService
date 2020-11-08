using AutoMapper;
using EnovaApi.Models.Common;
using EnovaApi.Models.Customer;
using Soneta.Business;
using Soneta.CRM;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        private static class Customer
        {
            public static void ConfigureMappings(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<Kontrahent, EnovaApi.Models.Customer.Customer>()
                    .IncludeBase<GuidedRow, GuidedEntity>()
                    .ForMember(d => d.Code, o => o.MapFrom(s => s.Kod))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Nazwa))
                    .ForMember(d => d.Nip, o => o.MapFrom(s => s.NIP))
                    .ForMember(d => d.MainAddress, o => o.MapFrom(s => s.Adres));
            }
        }
    }
}
