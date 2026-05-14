namespace WebApplication4.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> AuthorIds { get; set; }
    }
}
