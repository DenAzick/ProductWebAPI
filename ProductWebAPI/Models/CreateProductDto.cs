namespace ProductWebAPI.Models;

public class CreateProductDto
{
    public string Name { get; set; }
    public long Price { get; set; }
    public IFormFile PhotoUrl { get; set; }
    public Guid CategoryId { get; set; }

}
