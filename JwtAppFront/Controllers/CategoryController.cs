using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JwtAppFront.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwtAppFront.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<IActionResult> List()
    {

        var token = User.Claims.FirstOrDefault(x => x.Type == "accesstoken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetAsync("https://localhost:7238/Categories");
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = System.Text.Json.JsonSerializer.Deserialize<List<CategoryListModel>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return View(data);
            }
        }

        return View(new List<CategoryListModel>());
    }
    public async Task<IActionResult> Remove(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accesstoken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.DeleteAsync($"https://localhost:7238/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("List", "Category");
            }
            return View();

        }
        return View();

    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accesstoken")?.Value;
        if (token != null)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostAsync("https://localhost:7238/Categories", content);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("List", "Category");
            }

        }
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accesstoken")?.Value;
        if (token != null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetAsync($"https://localhost:7238/Categories/{id}");
            if (result.IsSuccessStatusCode)
            {
                var jsonData = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CategoryUpdateModel>(jsonData);
                return View(data);
            }
            return View();
        }
        return RedirectToAction("Login","Account");

    }
    [HttpPost]
    public async Task<IActionResult> Update(CategoryUpdateModel model)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accesstoken")?.Value;
        if (token != null)
        {
             var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result =await client.PutAsync($"https://localhost:7238/Categories",content);
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("List","Category");
            }
        }
        return RedirectToAction("Login","Account");
    }
}
