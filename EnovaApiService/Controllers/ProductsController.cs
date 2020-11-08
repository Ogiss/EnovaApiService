using Enova.Api;
using EnovaApiService.Enova;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Towary;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using EnovaApi.Models.Product;

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
    }
}
