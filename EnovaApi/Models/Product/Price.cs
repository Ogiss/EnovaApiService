using System;
using System.Collections.Generic;
using System.Text;

namespace EnovaApi.Models.Product
{
    public class Price
    {
        public Guid ProductGuid { get; set; }
        public Guid DefinitionGuid { get; set; }
        public decimal PriceWithoutTax { get; set; }
        public decimal PriceWithTax { get; set; }
    }
}
