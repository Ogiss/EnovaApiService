namespace EnovaApi.Models.Address
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public string AddressLine1 => $"{Street} {HouseNumber}/{ApartmentNumber}".TrimEnd('/', ' ');
        public string AddressLine2 => $"{PostalCode} {City}".Trim();
    }
}
