using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Journal.Data;
using Journal.Models;

namespace Journal.Pages.Scriptures
{
    public class EditModel : PageModel
    {
        private readonly Journal.Data.JournalContext _context;
        public List<Book> Books { get; set; }

        public EditModel(Journal.Data.JournalContext context)
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

            var scripture = await _context.Scriptures
      // .Include(s => s.Book)
       .FirstOrDefaultAsync(m => m.Id == id);
            if (scripture == null)
            {
                return NotFound();
            }
            Books = await _context.Books.ToListAsync();
            Scripture = scripture;
           ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
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

            _context.Attach(Scripture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScriptureExists(Scripture.Id))
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

        private bool ScriptureExists(int id)
        {
          return (_context.Scriptures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
