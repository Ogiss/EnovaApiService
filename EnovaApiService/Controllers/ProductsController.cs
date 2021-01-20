using Enova.Api;
using EnovaApiService.Enova;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Towary;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using EnovaApi.Models.Product;
using System.Collections.Generic;
using System.Data.SqlClient;
using EnovaApiService.Framework.Helpers;

namespace EnovaApiService.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("api/" + ResourcesNames.Products + "/{productGuid}/" + ProductAssociationsNames.CustomerPrices + "/{customerGuid}")]
        public async Task<IHttpActionResult> GetPriceForCustomer(Guid productGuid, Guid customerGuid)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);
                Towar towar = tm.Towary[productGuid];
                Kontrahent kontrahent = cm.Kontrahenci[customerGuid];

                var worker = new CenyKontrahentaWorker();
                worker.Towar = towar;
                worker.Kontrahent = kontrahent;
                worker.DefinicjaCeny = tm.DefinicjeCen["Hurtowa"];

                return Ok(new ProductPriceInfo
                {
                    ProductGuid = productGuid,
                    CustomerGuid = customerGuid,
                    Price = (decimal)worker.NettoPrzedRabatem.Value,
                    Rebate = worker.Rabat
                });
            }
        }

        [HttpGet]
        [Route("api/" + ResourcesNames.Products + "/" + ProductAssociationsNames.ModifiedPrices + "/{definitionGuid}/{stampFrom}/{stampTo}")]
        public IHttpActionResult GetModifiedPrices(Guid definitionGuid, long stampFrom, long stampTo)
        {
            var prices = new List<Price>();

            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                var sql =
                    $"SELECT t.Guid ProductGuid, d.Guid DefinitionGuid, c.NettoValue PriceWithoutTax, c.BruttoValue PriceWithTax " +
                    $"FROM Ceny c INNER JOIN DefinicjeCen d ON d.ID = c.Definicja INNER JOIN Towary t ON t.ID = c.Towar " +
                    $"WHERE d.Guid = '{definitionGuid}' AND CONVERT(BIGINT, c.Stamp) > {stampFrom} AND CONVERT(BIGINT, c.Stamp) <= {stampTo}";

                using (var reader = (SqlDataReader)connection.ExecuteCommand(Soneta.Business.App.ExecuteMode.Reader, sql))
                {
                    while (reader.Read())
                    {
                        prices.Add(new Price
                        {
                            ProductGuid = Sql.Read<Guid>(reader, "ProductGuid"),
                            DefinitionGuid = Sql.Read<Guid>(reader, "DefinitionGuid"),
                            PriceWithoutTax = Sql.Read<decimal>(reader, "PriceWithoutTax"),
                            PriceWithTax = Sql.Read<decimal>(reader, "PriceWithTax"),
                        });
                    }
                }
            }

            return Ok(prices);
        }
    }
}
