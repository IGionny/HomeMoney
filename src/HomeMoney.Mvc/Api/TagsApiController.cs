using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Tags/")]
  public class TagsApiController : BaseApiController
  {
    [HttpGet("Search/{tag}")]
    public async Task<IActionResult> SearchTags([FromRoute] string tag)
    {
      var hardEncodedList = new string[] {"Test", "Nice", "Baloon", "Green", "Yellow", "Fantastic"};
      return Ok(hardEncodedList.Where(x => x.Contains(tag, StringComparison.InvariantCultureIgnoreCase)).ToList());
    }
  }
}