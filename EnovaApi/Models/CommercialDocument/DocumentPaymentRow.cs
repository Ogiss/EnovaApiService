using System;

namespace EnovaApi.Models.CommercialDocument
{
    public class DocumentPaymentRow
    {
        public string PaymentMethod { get; set; }
        public DateTime DateOfPayment { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
