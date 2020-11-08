using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using EnovaApiService.Dto.Common;
using EnovaApiService.Dto.CRM;
using EnovaApiService.Enova;
using Soneta.Business;
using Soneta.CRM;

namespace EnovaApiService.Controllers
{
    public class KontrahenciController : ApiController
    {
        private const int KONTRAHENCI_PAGE_SIZE = 50;

        public IHttpActionResult Get(int id)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                CRMModule cm = CRMModule.GetInstance(session);
                Kontrahenci kontrahenci = cm.Kontrahenci;

                var kontrahent = kontrahenci[id];

                return Ok(new ResponseDto(KontrahentDto.FromEnova(kontrahent)));
            }
        }

        public IHttpActionResult Get(string kod)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                CRMModule cm = CRMModule.GetInstance(session);
                Kontrahenci kontrahenci = cm.Kontrahenci;

                var kontrahent = kontrahenci.WgKodu[kod];

                if (kontrahent != null)
                {
                    return Ok(new ResponseDto(KontrahentDto.FromEnova(kontrahent)));
                }

                return NotFound();
            }
        }

        public IHttpActionResult Get(ListRequestDto request)
        {
            using (Session session = EnovaClient.Login.CreateSession(true, false))
            {
                CRMModule cm = CRMModule.GetInstance(session);
                Kontrahenci kontrahenci = cm.Kontrahenci;
                var view = kontrahenci.CreateView();

                var pageSize = request.PageSize ?? KONTRAHENCI_PAGE_SIZE;
                var skip = request.PageNumber * pageSize;

                var rows = view.Skip(skip).Take(pageSize).Cast<Kontrahent>()
                    .Select(KontrahentListItemDto.FromEnova).ToArray();

                return Ok(new TotalCountResponseDto
                {
                    TotalCount = kontrahenci.CreateView().Count(),
                    Results = rows
                });
            }
        }
    }
}
