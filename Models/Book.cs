namespace Journal.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Scripture> scripture { get; set; }
    }
}
