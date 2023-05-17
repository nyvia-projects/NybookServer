namespace NybookApi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
        public int AuthorId { get; set; }
    }
}
