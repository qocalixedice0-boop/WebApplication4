namespace WebApplication4.DTOs
{
    public class CategoryResponseDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
