using EnovaApi.Models.Common;

namespace EnovaApi.Models.Customer
{
    public class Customer : GuidedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Email { get; set; }
        public long Stamp { get; set; }
        public int PaymentDeadlineInDays { get; set; }
        public WebAccount WebAccount { get; set; }
        public Address.Address MainAddress { get; set; }
    }
}
