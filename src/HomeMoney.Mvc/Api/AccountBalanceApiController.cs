using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/AccountBalance/")]
  public class AccountBalanceApiController : BaseCrudApiController<AccountBalance, AccountBalanceCrudService>
  {
    public AccountBalanceApiController(AccountBalanceCrudService accountBalanceCrudService) : base(
      accountBalanceCrudService)
    {
    }
  }
}