using APIServices.Context.BLOB_Storage;
using APIServices.Context.CV_Storage;
using APIServices.Entities.BLOB_Storage;
using APIServices.Entities.CV_Storage;
using APIServices.TransferObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

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
        public async Task<List<NewUserDTO>> GetUsers() 
        {
            var users = await _context.Users.Where(x=>x.IsActive == true).ToListAsync();

            List<NewUserDTO> usersDTO = new List<NewUserDTO>();

            foreach (var user in users) 
            {
                usersDTO.Add(new NewUserDTO
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    MobileNumber = user.MobileNumber,
                    Occupation = user.Occupation,
                    Area = user.Area,
                    IsActive = user.IsActive,
                    LinkedInProfileLink = user.LinkedInProfileLink,
                    GithubProfileLink = user.GithubProfileLink,
                    FileData = null,
                    FileName = null,
                    FileType = null,
                    PictureData = await _blobContext.Pictures.Where(x => x.UserID == user.UserID).Select(x => x.PictureData).FirstOrDefaultAsync()
                });
            }

            return usersDTO;
        }

        [HttpPost("SaveUser")]
        [RequestSizeLimit(100_000_000)]
        public async Task<Users> SaveUser([FromBody] NewUserDTO newUser)
        {
            Users result = new Users();

            var userExists = await _context.Users.Where(x => x.Email == newUser.Email).FirstOrDefaultAsync();

            if (userExists is null)
            {
                result.FirstName = newUser.FirstName;
                result.LastName = newUser.LastName;
                result.Email = newUser.Email;
                result.MobileNumber = newUser.MobileNumber;
                result.Occupation = newUser.Occupation;
                result.Area = newUser.Area;
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

            if(!string.IsNullOrWhiteSpace(newUser.FileData))
            {
                Documents doc = new Documents()
                {
                    UserID = result.UserID,
                    FileName = newUser?.FileName ?? "",
                    FileType = newUser?.FileType ?? "",
                    FileData = newUser?.FileData
                };

                await _blobContext.AddAsync(doc);
                await _blobContext.SaveChangesAsync();
            }

            if(!string.IsNullOrWhiteSpace(newUser.PictureData))
            {
                Pictures pic = new Pictures()
                {
                  PictureData = newUser.PictureData,
                  UserID = result.UserID
                };

                await _blobContext.AddAsync(pic);
                await _blobContext.SaveChangesAsync();
            }
            
          

            return result;
        }


        [HttpGet("GetUser/{id}")]
        public async Task<NewUserDTO> GetUsers(int id)
        {
                //Stored Proc to get the same info as below

                //            USE[CV_Storage]
                //GO
                //            /****** Object:  StoredProcedure [dbo].[GetProfileInfo]    Script Date: 2024/04/21 16:46:00 ******/
                //SET ANSI_NULLS ON
                //GO
                //            SET QUOTED_IDENTIFIER ON
                //GO
                //-- =============================================
                //--Author:		< Sumish Sewnarain >
                //--Create date: < 21 April 2024 >
                //--Description:	< Get profile and any Images or Documents linked >
                //-- =============================================
                //ALTER PROCEDURE[dbo].[GetProfileInfo]
                //@userID INT
                
                //AS
                //BEGIN
                
                //    --SET NOCOUNT ON added to prevent extra result sets from
                //    -- interfering with SELECT statements.
                
                //    SET NOCOUNT ON;
                
                //            --Insert statements for procedure here
                        
                //            DECLARE @User TABLE
                //            (
                //                UserID INT,
                //                FirstName nvarchar(MAX),
                //                LastName nvarchar(MAX),
                //                Email nvarchar(MAX),
                //                MobileNumber nvarchar(MAX),
                //            Occupation nvarchar(MAX),
                //                Area nvarchar(MAX),
                //                IsActive bit,
                //                LinkedInProfileLink nvarchar(MAX),
                //                GithubProfileLink nvarchar(MAX),
                //                FileName nvarchar(MAX),
                //                FileType nvarchar(MAX),
                //                FileData nvarchar(MAX),
                //                PictureData nvarchar(MAX)
                //            )
                        
                
                //            INSERT INTO @User
                        
                //            SELECT
                        
                //            U.UserID,
                //            U.FirstName,
                //            U.LastName,
                //            U.Email,
                //            U.MobileNumber,
                //            U.Occupation,
                //            U.Area,
                //            U.IsActive,
                //            U.LinkedInProfileLink,
                //            U.GithubProfileLink,
                //            D.FileName,
                //            D.FileType,
                //            D.FileData,
                //            ISNULL(P.PictureData, '')
                        
                //            FROM[CV_Storage].[dbo].[Users] U
                        
                //            JOIN[BLOB_Storage].[dbo].[Documents]  D ON D.UserID = U.UserID
                        
                //            JOIN[BLOB_Storage].[dbo].[Pictures] P ON P.UserID = U.UserID
                        
                //            WHERE U.UserID = @userID
                        
                
                //            SELECT * FROM @User
                //        END
        


            var users = await _context.Users.Where(x => x.UserID == id).FirstOrDefaultAsync();
            var docs = await _blobContext.Documents.Where(x => x.UserID == id).FirstOrDefaultAsync();
            var pics = await _blobContext.Pictures.Where(x => x.UserID == id).FirstOrDefaultAsync();


            NewUserDTO userDTO = new NewUserDTO()
            {
                UserID = users.UserID,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                MobileNumber = users.MobileNumber,
                Occupation = users.Occupation ?? "",
                Area = users.Area ?? "",
                IsActive = users.IsActive,
                LinkedInProfileLink = users.LinkedInProfileLink ?? "",
                GithubProfileLink = users.GithubProfileLink ?? "",
                FileName = docs?.FileName ?? "",
                FileType = docs?.FileType ?? "",
                FileData= docs?.FileData ?? "",
                PictureData = pics?.PictureData ?? ""
            };


            return userDTO;
        }

    }
}
