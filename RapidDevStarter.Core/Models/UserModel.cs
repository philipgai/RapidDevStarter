namespace RapidDevStarter.Core.Models
{
    public class UserModel
    {
        public int UserKey { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ContactInfoModel ContactInfo { get; set; }
    }
}