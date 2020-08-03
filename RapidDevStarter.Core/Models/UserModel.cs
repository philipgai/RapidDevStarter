using System;

namespace RapidDevStarter.Core.Models
{
    public class UserModel
    {
        public int UserKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ContactInfoModel ContactInfo { get; set; }
    }
}