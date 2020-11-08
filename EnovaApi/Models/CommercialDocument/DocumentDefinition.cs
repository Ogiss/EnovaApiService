using System;
using System.Collections.Generic;
using System.Text;
using EnovaApi.Models.Common;

namespace EnovaApi.Models.CommercialDocument
{
    public class DocumentDefinition : GuidedEntity
    {
        public DocumentCategory Category { get; set; }
        public string Symbol { get; set; }
        public string PrintTitle { get; set; }
        public string DateName { get; set; }
        public string OperationDateName { get; set; }
    }
}
