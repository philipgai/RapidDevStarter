using System;
using System.Collections.Generic;

namespace RapidDevStarter.Entities.RapidDevStarterEntities
{
    public partial class User
    {
        public int UserKey { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
