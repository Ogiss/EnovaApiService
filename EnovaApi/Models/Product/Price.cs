using System;

namespace EnovaApi.Models.Product
{
    public class Price
    {
        public Guid ProductGuid { get; set; }
        public Guid DefinitionGuid { get; set; }
        public decimal PriceWithoutTax { get; set; }
        public decimal PriceWithTax { get; set; }

        public override string ToString()
        {
            return $"Product guid:{ProductGuid}";
        }
    }
}
