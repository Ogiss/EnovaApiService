using EnovaApi.Models.Common;
using System;

namespace EnovaApi.Models.Customer
{
    public class Customer : GuidedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Email { get; set; }
        public long Stamp { get; set; }
        public int PaymentDeadlineInDays { get; set; }
        public WebAccount WebAccount { get; set; }
        public Address.Address MainAddress { get; set; }
        public DiscountGroup[] DiscountGroups { get; set; }

        public class DiscountGroup
        {
            public decimal Discount { get; set; }
            public bool Enabled { get; set; }
            public int GroupId { get; set; }
            public Guid GroupGuid { get; set; }
        }
    }
}
