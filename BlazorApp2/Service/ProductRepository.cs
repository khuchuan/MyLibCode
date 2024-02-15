using BlazorApp2.DTOs;

namespace BlazorApp2.Service;

public class ProductRepository
{
    public static List<Product> GetProducts()
    {
        List<Product> listData = new List<Product>();
        for (int i=0; i<32; i++)
        {
            Product product = new Product()
            {
                Name = "Viettel " + (i+1).ToString() + "0000",
                CreateTime = DateTime.Now,
                Description = $"Menh gia: {i + 1}0000",
                Id = i + 1,
                Price = (i + 1) * 10000
            };
            listData.Add(product);
        }

        return listData;
    }




}
