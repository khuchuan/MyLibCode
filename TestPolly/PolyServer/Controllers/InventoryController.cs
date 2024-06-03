using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace PollyServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private static int _requestCount;

    private readonly Product[] _products =
    {
        new()
        {
            Id = "1",
            Name = "Apples",
            Quantity = 12
        },
        new()
        {
            Id = "2",
            Name = "Oranges",
            Quantity = 25
        },
        new()
        {
            Id = "3",
            Name = "Oranges",
            Quantity = 25
        }
    };

    [HttpGet("{productId}")]
    public async Task<IActionResult> Get(string productId)
    {
        
        //await Task.Delay(100); // simulate some data processing by delaying for 100 milliseconds 
        _requestCount++;

        Console.WriteLine($"Lan goi: {_requestCount} -> Luc: {DateTime.Now}");

        var product = _products.FirstOrDefault(p => p.Id == productId);

        return _requestCount % 4 == 0
            ? // only one of out four requests will succeed
            Ok(product)
            : StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong");
    }

    [HttpGet("name/{productName}")]
    public async Task<IActionResult> GetByName(string productName)
    {
        await Task.Delay(100); // simulate some data processing by delaying for 100 milliseconds 

        return StatusCode(
            (int)HttpStatusCode.InternalServerError,
            $"Something went wrong when getting prodict by name when getting {productName}");
    }

}


public class Product
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }
}


