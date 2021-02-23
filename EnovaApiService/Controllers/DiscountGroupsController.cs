using Enova.Api;
using EnovaApi.Models.DiscountGroup;
using EnovaApiService.Enova;
using EnovaApiService.Enova.Repositories;
using EnovaApiService.Extensions.Soneta;
using System.Linq;
using System.Web.Http;

namespace EnovaApiService.Controllers
{
    public class DiscountGroupsController : ApiController
    {
        private readonly IDiscountGroupRepository _discountGroupRepository;

        public DiscountGroupsController()
        {
            _discountGroupRepository = Framework.DependencyProvider.Resolve<IDiscountGroupRepository>();
        }

        [HttpGet]
        [Route("api/" + ResourcesNames.DiscountGroupsByStamp + "/{priceDefId}/{stampFrom}/{stampTo}")]
        public IHttpActionResult GetAll(int priceDefId, long stampFrom, long stampTo)
        {
            return Ok(_discountGroupRepository.GetDiscountGroups(priceDefId, stampFrom, stampTo));
        }
    }
}
