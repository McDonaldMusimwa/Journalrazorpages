using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Journal.Data;
using Journal.Models;
using Microsoft.EntityFrameworkCore;

namespace Journal.Pages.Scriptures
{
    public class CreateModel : PageModel
    {
        private readonly Journal.Data.JournalContext _context;
        //public List<Book> Books { get; set; }
        public SelectList? Books { get; set; }
        public CreateModel(Journal.Data.JournalContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var books = await _context.Books.OrderBy(b => b.Title).ToListAsync();
            Books = new SelectList(books, nameof(Book.Id), nameof(Book.Title));
        }

        [BindProperty]
        public Scripture Scripture { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the dropdown in case of validation errors
                Console.WriteLine("ModelState is invalid:");
                await OnGetAsync();
                return Page();
            }


            _context.Scriptures.Add(Scripture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
