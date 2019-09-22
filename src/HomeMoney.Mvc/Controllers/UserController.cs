using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  public class UserController : Controller
  {
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
      // Clear the existing external cookie to ensure a clean login process
      // await HttpContext.SignOutAsync().ConfigureAwait(false);

      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }


    [AllowAnonymous]
    public IActionResult ExternalSignIn(string service, string returnUrl = null)
    {
      return Challenge(new AuthenticationProperties {RedirectUri = returnUrl ?? "/"}, service);
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
      return RedirectToAction("Login");
    }
  }
}