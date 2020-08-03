using System;
using System.Collections.Generic;

namespace RapidDevStarter.Entities.RapidDevStarterEntities
{
    public partial class User
    {
        public int UserKey { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
    }
}
