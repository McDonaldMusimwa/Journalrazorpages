namespace Journal.Models
{
    public class ScriptureWithBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Chapter { get; set; }
        public string Verse { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
