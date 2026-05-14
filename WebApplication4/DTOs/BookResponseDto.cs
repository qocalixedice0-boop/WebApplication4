namespace WebApplication4.DTOs
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }
}
