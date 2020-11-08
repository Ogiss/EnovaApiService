namespace EnovaApi.Models.CommercialDocument
{
    public enum DocumentCategory
    {
        NoValue = 0,
        TradeFirst = 1,
        Invoice = 2,
        InvoiceCorrection = 3,
        Purchase = 4,
        PurchaseCorrection = 5,

        Other = 0xffff
    }
}
