using System;
using EnovaApi.Models.Common;

namespace EnovaApi.Models.CommercialDocument
{
    public class CommercialDocumentRow : Entity
    {
        public int OrdinalNumber { get; set; }
        public Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public decimal QuantityTurnover { get; set; }
        public string QuantityTurnoverUnit { get; set; }
        public decimal PriceWithoutTaxAfterDiscount { get; set; }
        public decimal TaxPercent { get; set; }
        public string TaxName { get; set; }
        public decimal TotalValueWithoutTax { get; set; }
        public string QuantityFormated => $"{Quantity:#.##} {QuantityUnit}";
        public string QuantityTurnoverFormated => $"{QuantityTurnover:#.##} {QuantityTurnoverUnit}";
    }
}
