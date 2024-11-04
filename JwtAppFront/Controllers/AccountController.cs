using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using JwtAppFront.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace JwtAppFront.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _client;

    public AccountController(IHttpClientFactory client)
    {
        _client = client;
    }

    public IActionResult Login()
    {
        return View(new LoginModel());
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var client = _client.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://localhost:7238/Auths/Login", content);
            if (result.IsSuccessStatusCode)
            {
                var jsonData =await result.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<TokenModel>(jsonData,new JsonSerializerOptions{
                    PropertyNamingPolicy =JsonNamingPolicy.CamelCase
                });
                if(tokenModel !=null)
                {
                    JwtSecurityTokenHandler handler = new();
                    var token = handler.ReadJwtToken(tokenModel.Token);

                    var claims = token.Claims.ToList();
                    if(tokenModel.Token !=null)
                        claims.Add(new Claim("accesstoken",tokenModel.Token));
                    var claimsIdentity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);

                    var authProps = new AuthenticationProperties(){
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = model.isPersistant,
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProps);
                       return RedirectToAction("Index", "Home");
                }
                
                
             
            }
            else
            {
                ModelState.AddModelError("", "Kullanici Adi veya Şifre Hatali");
            }

        }
        return View();
    }
}
