namespace Journal.Models
{
    public class Scripture
    {
        public int Id { get; set; }
        public int BookId { get; set; } // Foreign key property
        /// <summary>
        /// public Book? Book { get; set; }   // Navigation property
        /// </summary>
        public int Chapter { get; set; }
        public string Verse { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
