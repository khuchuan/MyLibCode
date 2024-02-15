using BlazorAppTest.DTOs;

namespace BlazorAppTest.Service;

public class ProductRepository
{
    public static List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        for (int i=1; i<=16; i++)
        {
            Product product = new Product()
            {
                Name = "Viettel " + i.ToString() + "0000",
                CreateTime = DateTime.Now,
                Description = $"Menh gia: {i}0000",
                Id = i,
                Price = i * 10000
            };
            products.Add(product);
        }
        return products;
    }




}
