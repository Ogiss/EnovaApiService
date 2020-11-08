﻿using EnovaApi.Models.Address;
using EnovaApi.Models.Common;

namespace EnovaApi.Models.Customer
{
    public class Customer : GuidedEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public Address.Address MainAddress { get; set; }
    }
}
