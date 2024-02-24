using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Journal.Data;
using Journal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Scripting;

namespace Journal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly Journal.Data.JournalContext _context;
        public List<Book> Books { get; set; }
        public IndexModel(Journal.Data.JournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; } = default!;
        public IList<ScriptureWithBook> ScriptureWithBook { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchBook { get; set; }
        public SelectList Book { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Scriptures != null)
            {
                Books = await _context.Books.ToListAsync();
                var scripts = from s in _context.Scriptures
                                 join b in _context.Books on s.BookId equals b.Id
                                 select new ScriptureWithBook
                                 {
                                     Id = s.Id,
                                     BookId = s.BookId,
                                     BookTitle = b.Title,
                                     Chapter = s.Chapter,
                                     Verse = s.Verse,
                                     Text = s.Text,
                                     CreatedAt = s.CreatedAt
                                 };
                ScriptureWithBook = await scripts.ToListAsync();
            }
            //search by keyword
            /*
            IQueryable<string> bookQuery = from m in _context.Books
                                            orderby m.Title
                                            select m.Title;
            */
            var scriptures = from s in _context.Scriptures
                          join b in _context.Books on s.BookId equals b.Id
                          select new ScriptureWithBook
                          {
                              Id = s.Id,
                              BookId = s.BookId,
                              BookTitle = b.Title,
                              Chapter = s.Chapter,
                              Verse = s.Verse,
                              Text = s.Text,
                              CreatedAt = s.CreatedAt
                          };
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Text.Contains(SearchString));
            }

            
           
            if (!string.IsNullOrEmpty(SearchBook))
            {
                scriptures = scriptures.Where(b => b.BookTitle == SearchBook);
            }
           
            
            
                switch (SortBy)
                {
                    case "asc":
                        scriptures = scriptures.OrderBy(s => s.CreatedAt);
                        break;
                    case "desc":
                        scriptures = scriptures.OrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        // Default sorting logic
                        scriptures = scriptures.OrderByDescending(s => s.CreatedAt);
                        break;
                }
            
            ScriptureWithBook = await scriptures.ToListAsync();



        }
    }
}
