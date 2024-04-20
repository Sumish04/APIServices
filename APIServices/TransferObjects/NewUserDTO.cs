namespace APIServices.TransferObjects
{
    public class NewUserDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public string? LinkedInProfileLink { get; set; }
        public string? GithubProfileLink { get; set; }
        public string? FileName { get; set; }
        public string? FileType{ get; set; }
        public string? FileData { get; set; }
    }
}
