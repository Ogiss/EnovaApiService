using Enova.Api;
using EnovaApiService.Enova;
using EnovaApiService.Extensions;
using Soneta.Business;
using Soneta.CRM;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnovaApiService.Controllers
{
    public partial class CustomersController : ApiController
    {
        [HttpGet]
        [Route("api/" + ResourcesNames.Customers + "/{guid}")]
        public async Task<IHttpActionResult> Get(Guid guid)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                CRMModule crm = CRMModule.GetInstance(session);

                var customer = crm.Kontrahenci[guid];

                if (customer != null)
                {
                    return Ok(customer.ToDto());
                }

                return NotFound();
            }
        }
    }
}
