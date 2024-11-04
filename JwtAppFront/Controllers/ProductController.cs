using System.Net.Http.Headers;
using JwtAppFront.Extensions;
using JwtAppFront.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwtAppFront.Controllers;

public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<IActionResult> List()
    {

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.GetToken());
        var responseResult = await client.GetAsync("https://localhost:7238/Products");
        if(responseResult.IsSuccessStatusCode)
        {
            var jsonData = await responseResult.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ProductListModel>>(jsonData);
            return View(data);
        }
        return View();
    }
    public async Task<IActionResult> Remove(int id)
    {
        id = 5;
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.GetToken());
        var response = await client.DeleteAsync($"https://localhost:7238/Products/{id}");
        if(response.StatusCode != System.Net.HttpStatusCode.BadRequest)
        {
            return RedirectToAction("List","Product");
        }
        TempData["ErrorMessage"] = "Silinme işlemi sirasinda hata oldu";
        return RedirectToAction("List","Product");
    }
}
