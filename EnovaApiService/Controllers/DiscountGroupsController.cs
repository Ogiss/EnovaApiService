using Enova.Api;
using EnovaApi.Models.DiscountGroup;
using EnovaApiService.Enova;
using EnovaApiService.Extensions.Soneta;
using System.Linq;
using System.Web.Http;

namespace EnovaApiService.Controllers
{
    public class DiscountGroupsController : ApiController
    {
        [HttpGet]
        [Route("api/" + ResourcesNames.DiscountGroupsByPriceDef + "/{priceDefId}")]
        public IHttpActionResult GetAll(int priceDefId)
        {
            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                var results = connection.Query<DiscountGroup>(PrepareGetAllSql(priceDefId)).ToArray();

                return Ok(results);
            }
        }

        private string PrepareGetAllSql(int priceDefinitionId)
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
                    SELECT fd.ID, fd.Name, ('F.' + fd.Dictionary) Category, fd.Dictionary FROM FeatureDefs fd INNER JOIN FeatureDefIds ON fd.ID = FeatureDefIds.Id
                )
                ,DiscountGroups AS(
                    SELECT d.Id, d.Guid, d.Value Name, d.Category FROM Dictionary d INNER JOIN FeatureDefsCTE fd ON d.Category = fd.Category
                )
                SELECT* FROM DiscountGroups
                ORDER BY Category, Name";
        }
    }
}
