using EnovaApi.Models.DiscountGroup;
using System.Collections.Generic;

namespace EnovaApiService.Enova.Repositories
{
    public interface IDiscountGroupRepository
    {
        IEnumerable<int> GetCustomerDiscountIds(int priceDefId, long stampFrom, long stampTo);
        DiscountGroup[] GetDiscountGroups(int priceDefId, long stampFrom, long stampTo);
    }
}