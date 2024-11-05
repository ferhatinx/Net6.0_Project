using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JwtAppFront.Extensions;
using JwtAppFront.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> Create()
{
    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.GetToken());
    var responseMessage = await client.GetAsync("https://localhost:7238/Categories");

    if (responseMessage.IsSuccessStatusCode)
    {
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<List<CategoryListModel>>(jsonData);

        // Veriyi JSON formatında TempData'ya kaydediyoruz
        TempData["Categories"] = JsonConvert.SerializeObject(data);

        var categories = new SelectList(data, "Id", "Definition");
        var model = new ProductCreateModel { Categories = categories };
        return View(model);
    }

    TempData["ErrorMessage"] = "İşlem sırasında hata oluştu.";
    return RedirectToAction("List", "Product");
}
 [HttpPost]
 public async Task<IActionResult> Create(ProductCreateModel model)
 {
    if (TempData["Categories"] is string jsonCategories)
    {
        var data2 = JsonConvert.DeserializeObject<List<CategoryListModel>>(jsonCategories);
        model.Categories = new SelectList(data2, "Id", "Definition");
    }
    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.GetToken());
    var data = JsonConvert.SerializeObject(model);
    var content = new StringContent(data,Encoding.UTF8,"application/json");
    
    var responseMessage = await client.PostAsync("https://localhost:7238/Products",content);
    if(responseMessage.IsSuccessStatusCode)
    {
        return RedirectToAction("List","Product");
    }

    return View(model);
 }
}
