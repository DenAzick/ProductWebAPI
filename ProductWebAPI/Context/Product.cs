namespace ProductWebAPI.Context;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long  Price { get; set; }
    public string? PhotoUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
