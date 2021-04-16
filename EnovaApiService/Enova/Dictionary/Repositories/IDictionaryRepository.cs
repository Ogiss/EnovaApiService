using System;
using System.Collections.Generic;

namespace EnovaApiService.Enova.Dictionary.Repositories
{
    public interface IDictionaryRepository
    {
        IEnumerable<Guid> GetDeletedItemsGuids(DateTime stampFrom, DateTime stampTo);
    }
}