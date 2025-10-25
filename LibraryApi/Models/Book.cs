namespace LibraryApi.Models
{
    public class Book
    {
        public int Id { get; set; }                 // Primary key
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public int PublishedYear { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
