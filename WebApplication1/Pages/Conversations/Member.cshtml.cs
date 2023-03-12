using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ConversationAggregate;
using WebApplication1.RequestHelpers;

namespace PMS.Pages.Conversations
{
    public class MemberModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public MemberModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Conversation Conversation { get; set; }

        [BindProperty]
        public PagedList<ManageUser> Users { get; set; }

        [BindProperty]
        public PaginationParams paginationParams { get; set; } = new PaginationParams();



        public async Task<IActionResult> OnGetAsync(int? id, int p = 1, int s = 3)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = _context.ConversationUsers
                .Where(cu => cu.ConversationId == id)
                .Select(cu => cu.User);

            Users = PagedList<ManageUser>.ToPagedList(users, p, s);

            paginationParams.PageSize = s;
            paginationParams.PageNumber = p;
            paginationParams.Total = Users.MetaData.TotalCount;

            Conversation = await _context.Conversations
                .Include(c => c.Admin).FirstOrDefaultAsync(m => m.Id == id);

            if (Conversation == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.ManageUsers, "Id", "Id");
            return Page();
        }


        public void OnPostUpdateMem(string email, string conversationId)
        {
            System.Console.WriteLine("Hello");
        }


        public ActionResult OnGetDeleteMember(string userId, int conversationId)
        {
            _context.ConversationUsers.Remove(new ConversationUser { UserId = userId, ConversationId = conversationId });
            _context.SaveChanges();
            return Redirect($"Member?id={conversationId}");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public ActionResult OnPost(string email, string conversationId)
        {
            var alreadyIn = _context.ConversationUsers.Include(u => u.User).Where(cu => cu.User.Email == email);
            //ManageUser isExist = _context.Users.Where(u => u.Email.ToString() == email).SingleOrDefault();
            TempData["Error"] = "";
            bool flag = true;

            if (!alreadyIn.Any())
            {
                TempData["Error"] += "Can't find user";
                flag = false;
            }

            var isAlreadyinGroup = alreadyIn.Where(cu => cu.ConversationId == Int32.Parse(conversationId));
            if (isAlreadyinGroup.Any())
            {
                TempData["Error"] += "User already in conversation";
                flag = false;
            }

            if (flag == true)
            {

                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                _context.ConversationUsers.Add(new ConversationUser { User = user, ConversationId = Int32.Parse(conversationId) });
                _context.SaveChanges();
            }

            return Redirect($"Member?id={conversationId}");


        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.Id == id);
        }
    }
}
