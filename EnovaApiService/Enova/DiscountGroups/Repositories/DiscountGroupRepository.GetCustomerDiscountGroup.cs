using EnovaApi.Models.DiscountGroup;
using EnovaApiService.Extensions.Soneta;
using System.Collections.Generic;
using System.Linq;

namespace EnovaApiService.Enova.DiscountGroups.Repositories
{
    partial class DiscountGroupRepository
    {
        public IEnumerable<CustomerDiscountGroup> GetCustomerDiscount(int priceDefId, long stampFrom, long stampTo)
        {
            stampTo = stampTo > 0 ? stampTo : long.MaxValue;

            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                return connection.Query<CustomerDiscountGroup>(PrepareetCustomerDiscountGroupIdsSql(priceDefId, stampFrom, stampTo))?.ToArray();
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
                SELECT 
                    cg.Id,
					cg.Guid,
					d.ID DiscountGroupId,
					d.Guid DiscountGroupGuid,
					cg.Kontrahent CustomerId,
					k.Guid CustomerGuid,
					cg.Rabat Discount,
					cg.RabatZdefiniowany DiscountActive,
					CONVERT(BIGINT, cg.Stamp) Stamp
                FROM CenyGrupowe cg
                INNER JOIN Dictionary d ON d.ID = cg.GrupaTowarowa
                INNER JOIN FeatureDefsCTE fd ON fd.Category = d.Category
                INNER JOIN Kontrahenci k ON k.Id = cg.Kontrahent
                WHERE cg.Grupa IS NULL
                AND CONVERT(BIGINT, cg.Stamp) > {stampFrom} AND CONVERT(BIGINT, cg.Stamp) <= {stampTo}
                ORDER BY CONVERT(BIGINT, cg.Stamp)";
        }

        private class IdModel
        {
            public int Id { get; set; }
        }
    }
}
