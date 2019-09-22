using System;
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
      return View("Index");
    }
    
    [Route("/Accounts")]
    public IActionResult Accounts()
    {
      
      return View("Index");
    }
    
    [Route("/Accounts/Edit/{id:guid?}")]
    public IActionResult AccountEdit([FromRoute]Guid? id)
    {
      return View("Index");
    }
    
    [Route("/Categories")]
    public IActionResult Categories()
    {
      return View("Index");
    }
  }
}