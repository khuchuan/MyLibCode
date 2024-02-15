namespace BlazorApp2.DTOs;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public DateTime CreateTime { get; set; } = DateTime.Now;
}
