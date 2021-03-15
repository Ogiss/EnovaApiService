using AutoMapper;
using EnovaApi.Models.Common;
using EnovaApiService.Configuration;
using EnovaApiService.Enova.DiscountGroups.Repositories;
using EnovaApiService.Framework;
using Soneta.Business;
using Soneta.Towary;
using System;
using System.Linq;

namespace EnovaApiService.AutoMapper
{
    internal static partial class AutoMapperConfig
    {
        private static readonly Lazy<IDiscountGroupRepository> _discountGroupRepository =
            new Lazy<IDiscountGroupRepository>(() => { return DependencyProvider.Resolve<IDiscountGroupRepository>(); });

        public static class Product
        {
            public static void ConfigureMappings(IMapperConfigurationExpression cfg)
            {
                cfg.CreateMap<Towar, EnovaApi.Models.Product.Product>()
                    .IncludeBase<GuidedRow, GuidedEntity>()
                    .ForMember(d => d.Code, o => o.MapFrom(s => s.Kod))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Nazwa))
                    .ForMember(d => d.DiscountGroups, o => o.ConvertUsing(new DiscountGroupsConverter(), s => s.Features));
            }

            public class DiscountGroupsConverter : IValueConverter<FeatureCollection, GuidedEntity[]>
            {
                public GuidedEntity[] Convert(FeatureCollection sourceMember, ResolutionContext context)
                {
                    var discoutGroupFeatureDefs = _discountGroupRepository.Value.GetDiscountGroupFeatureDefs(AppSettings.DefaultPriceDef);

                    if (discoutGroupFeatureDefs != null)
                    {
                        var result = discoutGroupFeatureDefs
                            .Select(x => sourceMember.GetDictionaryItem(x.Name))
                            .Where(x => x != null)
                            .Select(x => new GuidedEntity { Id = x.ID, Guid = x.Guid })
                            .ToArray();

                        return result;
                    }

                    return Array.Empty<GuidedEntity>();
                }
            }
        }
    }
}
