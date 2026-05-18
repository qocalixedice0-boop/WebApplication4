namespace WebApplication4.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid  CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Author> Authors { get; set; } = new();
    }
}
