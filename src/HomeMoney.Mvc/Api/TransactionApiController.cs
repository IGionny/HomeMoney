using HomeMoney.Core.Domain;
using HomeMoney.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  [Route("api/Transaction/")]
  public class TransactionApiController : BaseCrudApiController<Transaction, TransactionCrudService>
  {
    public TransactionApiController(TransactionCrudService transactionCrudService) : base(transactionCrudService)
    {
    }
  }
}