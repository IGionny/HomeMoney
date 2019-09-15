using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Accounts/")]
  public class AccountApiController : BaseApiController
  {
    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
      var hardEncodedList = new List<EntityReference>();
      hardEncodedList.Add(new EntityReference(new Guid("90314B08-8CCA-4E5C-9D4F-72C82EA6009F"), "Fineco"));
      hardEncodedList.Add(new EntityReference(new Guid("D31EC7BD-79DA-4B61-AF15-01BFAFC1BBC3"), "Unicredit"));
      hardEncodedList.Add(new EntityReference(new Guid("82E828B6-2056-4A89-B49E-0AF727AA5438"), "Wallet"));
      hardEncodedList.Add(new EntityReference(new Guid("9873FF3E-8FE8-4C26-BCBD-22BC7C9D4184"), "PiggyBank"));
      return Ok(hardEncodedList.OrderBy(x=>x.Name));
    }
  }
}