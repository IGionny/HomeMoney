using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Categories/")]
  public class CategoryApiController : BaseApiController
  {
    [HttpGet("All")]
    public Task<IActionResult> All()
    {
      var hardEncodedList = new List<EntityReference>();
      hardEncodedList.Add(new EntityReference(new Guid("4DA52392-0DBC-4488-A6AD-EC5984DB5460"), "Grocery"));
      hardEncodedList.Add(new EntityReference(new Guid("4EC3DD5F-FBD5-434D-BA40-22D359225873"), "Various"));
      hardEncodedList.Add(new EntityReference(new Guid("92B8CD0E-A0B9-4AC3-B2DA-759C51A869BB"), "Gifts"));
      return Task.FromResult<IActionResult>(Ok(hardEncodedList.OrderBy(x => x.Name)));
    }
  }
}