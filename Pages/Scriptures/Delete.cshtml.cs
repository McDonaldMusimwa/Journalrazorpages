using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Journal.Data;
using Journal.Models;

namespace Journal.Pages.Scriptures
{
    public class DeleteModel : PageModel
    {
        private readonly Journal.Data.JournalContext _context;

        public DeleteModel(Journal.Data.JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Scripture Scripture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Scriptures == null)
            {
                return NotFound();
            }

            var scripture = await _context.Scriptures.FirstOrDefaultAsync(m => m.Id == id);

            if (scripture == null)
            {
                return NotFound();
            }
            else 
            {
                Scripture = scripture;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Scriptures == null)
            {
                return NotFound();
            }
            var scripture = await _context.Scriptures.FindAsync(id);

            if (scripture != null)
            {
                Scripture = scripture;
                _context.Scriptures.Remove(Scripture);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
