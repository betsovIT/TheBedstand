namespace TheBedstand.Data.Models
{
    public class BookGenre
    {
        public string BookId { get; set; }

        public Book Book { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
