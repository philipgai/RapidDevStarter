using System;

namespace RapidDevStarter.Api.DTOs
{
    public class UserDto
    {
        public int UserKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public virtual ContactInfoDto ContactInfo { get; set; }
    }
}