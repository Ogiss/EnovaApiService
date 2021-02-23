using EnovaApiService.Extensions.Soneta;
using System.Collections.Generic;
using System.Linq;

namespace EnovaApiService.Enova.Repositories
{
    partial class DiscountGroupRepository
    {
        public IEnumerable<int> GetCustomerDiscountIds(int priceDefId, long stampFrom, long stampTo)
        {
            stampTo = stampTo > 0 ? stampTo : long.MaxValue;

            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                return connection.Query<IdModel>(PrepareetCustomerDiscountGroupIdsSql(priceDefId, stampFrom, stampTo))?.Select(x => x.Id).ToArray();
            }
        }

        private string PrepareetCustomerDiscountGroupIdsSql(int priceDefId, long stampFrom, long stampTo)
        {
            return
                $@"WITH PriceDef AS(
                    SELECT * FROM DefinicjeCen dc
                    WHERE dc.Id = {priceDefId}
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
                    SELECT fd.ID, fd.Name, ('F.' + fd.Dictionary) Category, fd.Dictionary FROM FeatureDefs fd INNER JOIN FeatureDefIds ON fd.ID = FeatureDefIds.Id
                )
                SELECT cg.Id from CenyGrupowe cg
                INNER JOIN Dictionary d ON d.ID = cg.GrupaTowarowa
                INNER JOIN FeatureDefsCTE fd ON fd.Category = d.Category
                WHERE cg.Grupa IS NULL
                AND CONVERT(BIGINT, cg.Stamp) > {stampFrom} AND CONVERT(BIGINT, cg.Stamp) <= {stampTo}";
        }

        private class IdModel
        {
            public int Id { get; set; }
        }
    }
}
