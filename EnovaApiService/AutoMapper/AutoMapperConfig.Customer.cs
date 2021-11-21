using AutoMapper;
using EnovaApi.Models.Common;
using EnovaApi.Models.Customer;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Towary;
using System.Linq;

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
                    .ForMember(d => d.Email, o => o.MapFrom(s => s.EMAIL))
                    .ForMember(s => s.PaymentDeadlineInDays, o => o.MapFrom(s => s.Termin))
                    .ForMember(d => d.MainAddress, o => o.MapFrom(s => s.Adres))
                    .ForMember(d => d.WebAccount, o => o.ConvertUsing(new WebAccountConventer(), s => s))
                    .ForMember(d => d.DiscountGroups, o => o.ConvertUsing(new DiscountGroupsConventer(), s => s))
                    .ForMember(d => d.AgentPhone, o => o.MapFrom(s => s.Features["Telefon przedstawiciela"]));
            }

            public class WebAccountConventer : IValueConverter<Kontrahent, WebAccount>
            {
                public WebAccount Convert(Kontrahent sourceMember, ResolutionContext context)
                {
                    return new WebAccount
                    {
                        Login = (string)sourceMember.Features["WWW_LOGIN"],
                        Firstname = (string)sourceMember.Features["WWW_IMIE"],
                        LastName = (string)sourceMember.Features["WWW_NAZWISKO"],
                        Active = (bool?)sourceMember.Features["WWW_AKTYWNE"]
                    };
                }
            }

            public class DiscountGroupsConventer : IValueConverter<Kontrahent, EnovaApi.Models.Customer.Customer.DiscountGroup[]>
            {
                public EnovaApi.Models.Customer.Customer.DiscountGroup[] Convert(Kontrahent sourceMember, ResolutionContext context)
                {
                    var cenyGrupowe = (SubTable<CenaGrupowa>)sourceMember.CenyGrupowe;

                    return cenyGrupowe.Where(x => x.Grupa == null && x.GrupaTowarowa != null).Select(x => new EnovaApi.Models.Customer.Customer.DiscountGroup
                    {
                        GroupId = x.GrupaTowarowa.ID,
                        GroupGuid = x.GrupaTowarowa.Guid,
                        Discount = x.Rabat,
                        Enabled = x.RabatZdefiniowany
                    }).ToArray();
                }
            }
        }
    }
}
