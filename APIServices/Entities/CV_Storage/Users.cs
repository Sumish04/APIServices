using System.ComponentModel.DataAnnotations;

namespace APIServices.Entities.CV_Storage
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public string? LinkedInProfileLink { get; set; }
        public string? GithubProfileLink { get; set; }
    }
}
