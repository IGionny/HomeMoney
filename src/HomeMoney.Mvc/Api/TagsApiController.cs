using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Models;
using HomeMoney.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Tag/")]
  public class TagsApiController : BaseCrudApiController<UserTag, UserTagCrudService>
  {
    [HttpGet("Search/{tag}")]
    public async Task<IActionResult> SearchTags([FromRoute] string tag)
    {
      var pagedRequest = new PagedRequest();
      pagedRequest.Page = 0;
      pagedRequest.PageSize = 100;
      pagedRequest.AddFilter(nameof(UserTag.Name), tag);
      var result = await this._absCrudService.PagedAsync(pagedRequest).ConfigureAwait(false);
      return Ok(result.Value);
    }

    public TagsApiController(UserTagCrudService userTagCrudService) : base(userTagCrudService)
    {
    }
  }
}