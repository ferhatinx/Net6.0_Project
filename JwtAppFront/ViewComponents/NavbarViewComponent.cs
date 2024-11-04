using Microsoft.AspNetCore.Mvc;

namespace JwtAppFront.ViewComponents;

public class NavbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
