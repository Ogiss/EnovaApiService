using System.Web.Http;
using System.Reflection;
using Soneta.Business;
using EnovaApiService.Enova;

namespace EnovaApiService.Controllers
{
    [RoutePrefix("api/system")]
    public class SystemController : ApiController
    {
        [HttpGet]
        [Route("version")]
        public string Get()
        {
            return $"Enova Api ver: {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        [HttpGet]
        [Route("getdbts")]
        public long GetDbts()
        {
            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                return (long)connection.ExecuteCommand(Soneta.Business.App.ExecuteMode.Scalar, "SELECT CONVERT(BIGINT, @@DBTS);");
            }
        }
    }
}
