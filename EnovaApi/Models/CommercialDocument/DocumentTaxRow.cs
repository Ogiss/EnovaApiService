namespace EnovaApi.Models.CommercialDocument
{
    public class DocumentTaxRow
    {
        public decimal TaxPercent { get; set; }
        public string TaxName { get; set; }
        public decimal TotalValueWithoutTax { get; set; }
        public decimal TotalValueTax { get; set; }
        public decimal TotalValueWithTax { get; set; }
    }
}
