using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}