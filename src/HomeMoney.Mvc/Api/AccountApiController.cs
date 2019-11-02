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
  [Route("api/Account/")]
  public class AccountApiController : BaseCrudApiController<Account, AccountCrudService>
  {
    public AccountApiController(AccountCrudService accountCrudService) : base(accountCrudService)
    {
      if (!accountCrudService.InMemoryEntities.Any())
      {
        //Push test data

        accountCrudService.SaveAsync(new Account() {Name = "Fineco"}, this.GetUserIdentity());
        accountCrudService.SaveAsync(new Account() {Name = "Unicredit"}, this.GetUserIdentity());
        accountCrudService.SaveAsync(new Account() {Name = "Wallet"}, this.GetUserIdentity());
        accountCrudService.SaveAsync(new Account() {Name = "PiggyBank"}, this.GetUserIdentity());
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