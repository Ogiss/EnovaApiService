using Enova.Api;
using EnovaApiService.Enova;
using Soneta.Business;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Soneta.Handel;
using EnovaApiService.Extensions;

namespace EnovaApiService.Controllers
{
    public partial class CommercialDocumentController : ApiController
    {
        [HttpGet]
        [Route("api/" + ResourcesNames.CommercialDocuments + "/{documentGuid}")]
        public async Task<IHttpActionResult> Get(Guid documentGuid)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                HandelModule hm = HandelModule.GetInstance((session));

                var document = hm.DokHandlowe[documentGuid];

                if (document != null)
                {
                    var ret = document.ToDto();
                    return Ok(ret);
                }

                return NotFound();
            }
        }
    }
}
