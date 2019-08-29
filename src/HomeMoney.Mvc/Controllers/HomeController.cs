using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  public class HomeController : Controller
  {
    
    public IActionResult Index()
    {
      return View("Index");
    }
    
    [Route("/Transactions")]
    public IActionResult Transactions()
    {
      return Index();
    }
    
    [Route("/Accounts")]
    public IActionResult Accounts()
    {
      return Index();
    }
    
    [Route("/Categories")]
    public IActionResult Categories()
    {
      return Index();
    }
  }
}