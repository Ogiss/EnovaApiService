using Enova.Api;
using EnovaApiService.Enova;
using EnovaApiService.Extensions;
using Soneta.Business;
using Soneta.CRM;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using EnovaApiService.Extensions.Soneta;
using System.Linq;

namespace EnovaApiService.Controllers
{
    public class CustomersController : ApiController
    {
        [HttpGet]
        [Route("api/" + ResourcesNames.Customers + "/{guid}")]
        public IHttpActionResult Get(Guid guid)
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

        [HttpGet]
        [Route("api/"+ ResourcesNames.CustomersByStamp + "/{stampFrom}/{stampTo}")]
        public IHttpActionResult GetByStamp(long stampFrom, long stampTo)
        {
            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                var sql =
                    $"SELECT Guid FROM Kontrahenci " +
                    $"WHERE CONVERT(BIGINT, Stamp) > {stampFrom} AND CONVERT(BIGINT, Stamp) <= {stampTo} ORDER BY Stamp";

                var guids = connection.Query<GuidRow>(sql).ToArray();

                return Ok(guids.Select(x => x.Guid));
            }
        }

        private class GuidRow
        {
            public Guid Guid { get; set; }
        }

    }
}
