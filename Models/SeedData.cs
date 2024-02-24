
using Journal.Data;
using Microsoft.EntityFrameworkCore;

namespace Journal.Models
{
    public class SeedData
    {

        private static void AddBooks(JournalContext context, List<string> bookTitles)
        {
            foreach (var title in bookTitles)
            {
                context.Books.Add(new Book { Title = title });
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new JournalContext(
                           serviceProvider.GetRequiredService<DbContextOptions<JournalContext>>()))
            {
                // Check if any scriptures exist
                if (context.Scriptures.Any())
                {
                    return;   // DB has been seeded
                }
                var genesis = new Book { Title = "Genesis" };
                List<string> Books = new List<string>
        {
            "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel",
            "2 Samuel", "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job",
            "Psalms", "Proverbs", "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah", "Lamentations",
            "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk",
            "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts", "Romans",
            "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians",
            "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon", "Hebrews",
            "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation","1 Nephi", "2 Nephi", "Jacob", "Enos", "Jarom", "Omni", "Words of Mormon", "Mosiah", "Alma", "Helaman",
            "3 Nephi", "4 Nephi", "Mormon", "Ether", "Moroni","Book of Moses", "Book of Abraham", "Joseph Smith—Matthew", "Joseph Smith—History", "Articles of Faith","D&C"
        };

                AddBooks(context, Books);
                context.SaveChanges();
                // Seed initial scriptures
                var books = context.Books.ToList();
                context.Scriptures.AddRange(
    new Scripture
    {
        BookId =  1,
        Chapter = 6,
        Verse = "33",
        Text = "But seek first his kingdom and his righteousness, and all these things will be given to you as well.",
        CreatedAt = DateTime.Now
    },
    new Scripture
    {
        BookId = 2,
        Chapter = 3,
        Verse = "5-6",
        Text = "Trust in the LORD with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.",
        CreatedAt = DateTime.Now
    },
    new Scripture
    {
        BookId =  3 ,
        Chapter = 40,
        Verse = "31",
        Text = "But those who hope in the LORD will renew their strength. They will soar on wings like eagles; they will run and not grow weary, they will walk and not be faint.",
        CreatedAt = DateTime.Now
    },
    new Scripture
    {
        BookId = 4,
        Chapter = 8,
        Verse = "28",
        Text = "And we know that in all things God works for the good of those who love him, who have been called according to his purpose.",
        CreatedAt = DateTime.Now
    },
    new Scripture
    {
        BookId = 5 ,
        Chapter = 29,
        Verse = "11",
        Text = "For I know the plans I have for you,” declares the LORD, “plans to prosper you and not to harm you, plans to give you hope and a future.",
        CreatedAt = DateTime.Now
    }
);
                context.SaveChanges();
 
            }
        }
    }
}

