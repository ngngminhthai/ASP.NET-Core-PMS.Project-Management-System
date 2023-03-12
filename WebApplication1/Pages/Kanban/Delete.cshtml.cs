using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities;
using WebApplication1.Data;

namespace PMS.Pages.Kanban
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public DeleteModel(WebApplication1.Data.ManageAppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public KanbanColume KanbanColume { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KanbanColume = await _context.kanbanColumes
                .Include(k => k.project).FirstOrDefaultAsync(m => m.Id == id);

            if (KanbanColume == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KanbanColume = await _context.kanbanColumes.FindAsync(id);

            if (KanbanColume != null)
            {
                _context.kanbanColumes.Remove(KanbanColume);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
