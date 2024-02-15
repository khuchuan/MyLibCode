namespace BlazorAppTest.DTOs;

public class Products
{
    public Products() 
    { 
    }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }
    public List<Product> ListProduct { get; set; }
}
