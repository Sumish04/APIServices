using APIServices.Context.BLOB_Storage;
using APIServices.Context.CV_Storage;
using APIServices.Entities.BLOB_Storage;
using APIServices.Entities.CV_Storage;
using APIServices.TransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIServices.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class CVManagerController : ControllerBase
    {
        private readonly CVDbContext _context;
        private readonly BlobDbContext _blobContext;

        public CVManagerController(CVDbContext context, BlobDbContext blobContext)
        {
            _context = context;
            _blobContext = blobContext;
        }


        [HttpGet("GetUsers")]
        public async Task<List<Users>> GetUsers() 
        {
            var users = await _context.Users.Where(x=>x.IsActive == true).ToListAsync();

            return users;
        }

        [HttpPost("SaveUser")]
        public async Task<Users> SaveUser([FromBody] NewUserDTO newUser)
        {
            Users result = new Users();
            Documents doc = new Documents();

            var userExists = await _context.Users.Where(x => x.Email == newUser.Email).FirstOrDefaultAsync();

            if (userExists is null)
            {
                result.FirstName = newUser.FirstName;
                result.LastName = newUser.LastName;
                result.Email = newUser.Email;
                result.MobileNumber = newUser.MobileNumber;
                result.IsActive = true;
                result.LinkedInProfileLink = newUser.LinkedInProfileLink;
                result.GithubProfileLink = newUser.GithubProfileLink;
            }
            else
            {
                return result;
            }     

            await _context.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
        }    

    }
}
