using Enova.Api;
using EnovaApiService.Enova.Dictionary.Repositories;
using System;
using System.Web.Http;

namespace EnovaApiService.Controllers
{
    public class DictionaryItemsController : ApiController
    {
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryItemsController()
        {
            _dictionaryRepository = Framework.DependencyProvider.Resolve<IDictionaryRepository>();
        }

        [HttpGet]
        [Route("api/" + ResourcesNames.DictionaryItemsDeleted + "/{stampFrom}/{stampTo}")]
        public IHttpActionResult GetDeletedItemsGuids(DateTime stampFrom, DateTime stampTo)
        {
            return Ok(_dictionaryRepository.GetDeletedItemsGuids(stampFrom, stampTo));
        }
    }
}
