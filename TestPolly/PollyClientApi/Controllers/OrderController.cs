﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PollyClientApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById(string productId)
    {
        var httpClient = _httpClientFactory.CreateClient("InventoryService");
        var httpResponseMessage = await httpClient.GetAsync($"api/inventory/{productId}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var product = await httpResponseMessage.Content.ReadFromJsonAsync<Product>();

            return Ok(product);
        }

        return StatusCode((int)httpResponseMessage.StatusCode, await httpResponseMessage.Content.ReadAsStringAsync());
    }


    [HttpGet("name/{productName}")]
    public async Task<IActionResult> GetByName(string productName)
    {
        var httpClient = _httpClientFactory.CreateClient("InventoryService");
        var httpResponseMessage = await httpClient.GetAsync($"api/inventory/name/{productName}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var product = await httpResponseMessage.Content.ReadFromJsonAsync<Product>();

            return Ok(product);
        }

        return StatusCode((int)httpResponseMessage.StatusCode, await httpResponseMessage.Content.ReadAsStringAsync());
    }

}
