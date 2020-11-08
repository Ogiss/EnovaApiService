using System;
using System.Collections.Generic;
using EnovaApi.Models.Common;
using EnovaApi.Models.Customer;

namespace EnovaApi.Models.CommercialDocument
{
    public class CommercialDocument : GuidedEntity
    {
        public DocumentDefinition Definition { get; set; }
        public DocumentState State { get; set; }
        public DateTime Date { get; set; }
        public DateTime OperationDate { get; set; }
        public string DocumentNumber { get; set; }
        public Customer.Customer Customer { get; set; }
        public decimal TotalValueWithoutTax { get; set; }
        public decimal TotalValueTax { get; set; }
        public decimal TotalValueWithTax { get; set; }
        public string TotalValueWithTaxInWords { get; set; }
        public IEnumerable<CommercialDocumentRow> Rows { get; set; }
        public IEnumerable<DocumentTaxRow> TaxesSummary { get; set; }
        public IEnumerable<DocumentPaymentRow> PaymentSummary { get; set; }
    }
}
