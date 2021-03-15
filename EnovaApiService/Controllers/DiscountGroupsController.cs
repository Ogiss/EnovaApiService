using Enova.Api;
using EnovaApiService.Enova.DiscountGroups.Repositories;
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
