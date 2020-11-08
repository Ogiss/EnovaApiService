using System;

namespace EnovaApi.Models.Product
{
    public class ProductPriceInfo
    {
        public Guid ProductGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public decimal Price { get; set; }
        public decimal Rebate { get; set; }
    }
}
