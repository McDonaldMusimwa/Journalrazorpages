using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Journal.Models;

namespace Journal.Data
{
    public class JournalContext : DbContext
    {
        public JournalContext(DbContextOptions<JournalContext> options)
            : base(options)
        {
        }

        public DbSet<Scripture> Scriptures { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scripture>()
                .HasOne<Book>()            // A Scripture has one Book
        .WithMany()                // A Book can have many Scriptures
        .HasForeignKey(s => s.BookId);

        }
    }
}
