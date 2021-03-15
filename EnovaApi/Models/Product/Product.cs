using EnovaApi.Models.Common;

namespace EnovaApi.Models.Product
{
    public class Product : GuidedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public GuidedEntity[] DiscountGroups { get; set; }
    }
}
