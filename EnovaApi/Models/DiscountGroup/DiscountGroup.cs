using System;

namespace EnovaApi.Models.DiscountGroup
{
    public class DiscountGroup
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
