using AutoMapper;
using EnovaApi.Models.Address;
using Soneta.Core;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        private static class Address
        {
            public static void ConfigureMappings(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<Adres, EnovaApi.Models.Address.Address>()
                    .ForMember(d => d.Street, o => o.MapFrom(s => s.Ulica))
                    .ForMember(d => d.HouseNumber, o => o.MapFrom(s => s.NrDomu))
                    .ForMember(d => d.ApartmentNumber, o => o.MapFrom(s => s.NrLokalu))
                    .ForMember(d => d.PostalCode, o => o.MapFrom(s => s.KodPocztowyS))
                    .ForMember(d => d.City, o => o.MapFrom(s => s.Miejscowosc));
            }
        }
    }
}
