using EnovaApi.Models.Common;
using EnovaApiService.Extensions.Soneta;
using System;
using System.Linq;
using System.Runtime.Caching;

namespace EnovaApiService.Enova.DiscountGroups.Repositories
{
    internal partial class DiscountGroupRepository
    {
        public FeatureDef[] GetDiscountGroupFeatureDefs(int priceDefId)
        {
            ObjectCache cache = MemoryCache.Default;
            string key = $"DiscountGroupFeatureDefs_{priceDefId}";

            FeatureDef[] defs = cache[key] as FeatureDef[];

            if (defs == null)
            {
                using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
                {
                    defs = connection.Query<FeatureDef>(PrepareGetDiscountGroupFeatureDefsSql(priceDefId)).ToArray();
                }

                if (defs != null)
                {
                    cache.Add(key, defs, DateTimeOffset.Now.AddHours(2));
                }
            }

            return defs;
        }

        private string PrepareGetDiscountGroupFeatureDefsSql(int priceDefinitionId)
        {
            return
                $@"WITH PriceDef AS(
                    SELECT * FROM DefinicjeCen dc
                    WHERE dc.Id = {priceDefinitionId}
                )
                ,FeatureDefIds(Id) AS(
                    SELECT Rabat1GrupaTowarowa FROM PriceDef
                    UNION ALL
                    SELECT Rabat2GrupaTowarowa FROM PriceDef
                    UNION ALL
                    SELECT Rabat3GrupaTowarowa FROM PriceDef
                    UNION ALL
                    SELECT Rabat4GrupaTowarowa FROM PriceDef
                    UNION ALL
                    SELECT Rabat5GrupaTowarowa FROM PriceDef
                )
                ,FeatureDefsCTE AS(
                    SELECT fd.ID, fd.Guid, fd.Name FROM FeatureDefs fd INNER JOIN FeatureDefIds ON fd.ID = FeatureDefIds.Id
                )
                SELECT * FROM FeatureDefsCTE
                ORDER BY Name";
        }
    }
}
