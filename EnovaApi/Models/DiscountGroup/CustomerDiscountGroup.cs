using System;

namespace EnovaApi.Models.DiscountGroup
{
    public class CustomerDiscountGroup
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int DiscountGroupId { get; set; }
        public Guid DiscountGroupGuid { get; set; }
        public int CustomerId { get; set; }
        public Guid CustomerGuid { get; set; }
        public decimal Discount { get; set; }
        public bool DiscountActive { get; set; }
        public long Stamp { get; set; }
    }
}
