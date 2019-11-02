using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Models;
using HomeMoney.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Category/")]
  public class CategoryApiController : BaseCrudApiController<Category, CategoryCrudService>
  {
    public CategoryApiController(CategoryCrudService categoryCrudService) : base(categoryCrudService)
    {
      if (!categoryCrudService.InMemoryEntities.Any())
      {
        //Push test data

        categoryCrudService.SaveAsync(new Category() {Name = "Grocery"}, this.GetUserIdentity());
        categoryCrudService.SaveAsync(new Category() {Name = "Various"}, this.GetUserIdentity());
        categoryCrudService.SaveAsync(new Category() {Name = "Gifts"}, this.GetUserIdentity());
      }
    }


    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
      var pagedRequest = new PagedRequest();
      pagedRequest.Page = 0;
      pagedRequest.PageSize = 100;
      var result = await this._absCrudService.PagedAsync(pagedRequest).ConfigureAwait(false);
      return Ok(result.Value);
    }
  }
}