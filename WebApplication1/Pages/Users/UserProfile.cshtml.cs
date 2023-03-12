using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Application.ViewModels;
using PMS.Services;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace PMS.Pages.Users
{
    public class UserProfileModel : PageModel
    {
        private readonly ManageAppDbContext _dbContext;
        private readonly UserManager<ManageUser> _userManager;
        private readonly IFileUploadService fileUploadService;

        [BindProperty]
        public ManageUser User { get; set; }

        public UserProfileModel(ManageAppDbContext dbContext, UserManager<ManageUser> userManager, IFileUploadService fileUploadService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            this.fileUploadService = fileUploadService;
        }

        public async Task OnGetAsync()
        {
            var name = HttpContext.User.Identity.Name;
            User = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            UploadedFileViewModel uploadedFileViewModel = new UploadedFileViewModel { UploaderName = HttpContext.User.Identity.Name, UploadedPosition = Data.Enums.UploadedPosition.UserProfile };
            var filePath = await fileUploadService.UploadFile(file, uploadedFileViewModel);


            var user = _dbContext.Users.FirstOrDefault(p => p.Id == User.Id);
            user.ImageProfile = filePath;
            user.Email = User.Email;
            user.UserName = User.UserName;

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            return Page();
        }
    }
}
