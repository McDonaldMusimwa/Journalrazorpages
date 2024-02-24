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
    public class DetailsModel : PageModel
    {
        private readonly Journal.Data.JournalContext _context;

        public DetailsModel(Journal.Data.JournalContext context)
        {
            _context = context;
        }

      public Scripture Scripture { get; set; } = default!; 
        public ScriptureWithBook ScriptureWithBook { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scripture = await (from s in _context.Scriptures
                                   join b in _context.Books on s.BookId equals b.Id
                                   where s.Id == id
                                   select new ScriptureWithBook
                                   {
                                       Id = s.Id,
                                       BookId = s.BookId,
                                       BookTitle = b.Title,
                                       Chapter = s.Chapter,
                                       Verse = s.Verse,
                                       Text = s.Text,
                                       CreatedAt = s.CreatedAt
                                   }).FirstOrDefaultAsync();

            if (scripture == null)
            {
                return NotFound();
            }

            ScriptureWithBook = scripture;
            return Page();
        }
    }
}
