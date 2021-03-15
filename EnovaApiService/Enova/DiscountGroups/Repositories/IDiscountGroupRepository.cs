using EnovaApi.Models.Common;
using EnovaApi.Models.DiscountGroup;
using System.Collections.Generic;

namespace EnovaApiService.Enova.DiscountGroups.Repositories
{
    public interface IDiscountGroupRepository
    {
        IEnumerable<CustomerDiscountGroup> GetCustomerDiscount(int priceDefId, long stampFrom, long stampTo);
        DiscountGroup[] GetDiscountGroups(int priceDefId, long stampFrom, long stampTo);
        FeatureDef[] GetDiscountGroupFeatureDefs(int priceDefId);
    }
}