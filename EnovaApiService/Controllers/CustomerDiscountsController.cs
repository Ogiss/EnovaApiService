using Enova.Api;
using EnovaApiService.Enova.DiscountGroups.Repositories;
using System.Web.Http;

namespace EnovaApiService.Controllers
{
    public class CustomerDiscountsController : ApiController
    {
        private readonly IDiscountGroupRepository _discountGroupRepository;

        public CustomerDiscountsController()
        {
            _discountGroupRepository = Framework.DependencyProvider.Resolve<IDiscountGroupRepository>();
        }

        [HttpGet]
        [Route("api/" + ResourcesNames.CustomerDiscountsByStamp + "/{priceDefId}/{stampFrom}/{stampTo}")]
        public IHttpActionResult GetCustomerDiscountsByStamp(int priceDefId, long stampFrom, long stampTo)
        {
            return Ok(_discountGroupRepository.GetCustomerDiscount(priceDefId, stampFrom, stampTo));
        }
    }
}
