using System;
using System.Collections.Generic;

namespace RapidDevStarter.Entities.RapidDevStarterEntities
{
    public partial class DboContactInfo
    {
        public int ContactInfoUserKey { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime RowStart { get; set; }
        public DateTime RowEnd { get; set; }
    }
}
