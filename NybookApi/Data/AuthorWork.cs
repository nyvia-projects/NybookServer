using CsvHelper.Configuration;

namespace NybookApi.Data {
    public class AuthorWork
    {
        public string AuthorName { get; set; }
        public int AuthorAge { get; set; }
        public int AuthorRating { get; set; }
        public string BookTitle { get; set; }
        public int BookYear { get; set; }
        public int BookRating { get; set; }
    }

    public sealed class AuthorWorkMap : ClassMap<AuthorWork>
    {
        public AuthorWorkMap()
        {
            Map(m => m.AuthorName).Name("Author Name");
            Map(m => m.AuthorAge).Name("Author Age");
            Map(m => m.AuthorRating).Name("Author Rating");
            Map(m => m.BookTitle).Name("Book Title");
            Map(m => m.BookYear).Name("Book Year");
            Map(m => m.BookRating).Name("Book Rating");
        }
    }

}
