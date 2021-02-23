using Enova.Api;
using EnovaApiService.Enova.Repositories;
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
        public IHttpActionResult GetCustomerDiscountsIdsByStamp(int priceDefId, long stampFrom, long stampTo)
        {
            return Ok(_discountGroupRepository.GetCustomerDiscountIds(priceDefId, stampFrom, stampTo));
        }
    }
}
