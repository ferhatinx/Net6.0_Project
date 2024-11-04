using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace JwtAppFront.Extensions;

public static class HttpClientExtension
{
    public static string GetToken(this Controller controller)
    {
        var token = controller.User.Claims.FirstOrDefault(x=>x.Type =="accesstoken")?.Value;
        if(token!=null)
        {
            return token;
        }
        return "";
        
    }
}
