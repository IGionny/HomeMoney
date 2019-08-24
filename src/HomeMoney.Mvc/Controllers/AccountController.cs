using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }
  }
}