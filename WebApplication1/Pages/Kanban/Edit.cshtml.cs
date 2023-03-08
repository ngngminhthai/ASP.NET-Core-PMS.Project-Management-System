using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities;
using WebApplication1.Data;

namespace PMS.Pages.Kanban
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.ManageAppDbContext _context;

        public EditModel(WebApplication1.Data.ManageAppDbContext context)
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
           ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(KanbanColume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanbanColumeExists(KanbanColume.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool KanbanColumeExists(int id)
        {
            return _context.kanbanColumes.Any(e => e.Id == id);
        }
    }
}
