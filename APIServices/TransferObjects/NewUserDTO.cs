using Microsoft.EntityFrameworkCore;

namespace APIServices.TransferObjects
{
    [Keyless]
    public class NewUserDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Area { get; set; }
        public bool IsActive { get; set; }
        public string? LinkedInProfileLink { get; set; }
        public string? GithubProfileLink { get; set; }
        public string? FileName { get; set; }
        public string? FileType{ get; set; }
        public string? FileData { get; set; }
        public string? PictureData { get; set;}
    }
}
