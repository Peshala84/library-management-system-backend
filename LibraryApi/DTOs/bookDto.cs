namespace LibraryApi.DTOs
{
    public class BookDto
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public int PublishedYear { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
