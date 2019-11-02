using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Models;
using HomeMoney.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
  public abstract class BaseCrudApiController<T, TService> : BaseApiController
    where T : UserEntity where TService : AbsCrudService<T>
  {
    protected readonly AbsCrudService<T> _absCrudService;

    protected BaseCrudApiController(AbsCrudService<T> absCrudService)
    {
      _absCrudService = absCrudService ?? throw new ArgumentNullException(nameof(absCrudService));
    }

    [HttpGet("id:{guid}")]
    public virtual async Task<IActionResult> Get([FromRoute] Guid id)
    {
      var entity = await _absCrudService.GetAsync(id).ConfigureAwait(false);
      if (entity == null) return NotFound();
      return Ok(entity);
    }


    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] T entity)
    {
      var saveResult = await _absCrudService.SaveAsync(entity, GetUserIdentity()).ConfigureAwait(false);
      if (saveResult.IsValid) return Ok(entity);

      return BadRequest(saveResult.ToString());
    }

    [HttpPut]
    public virtual async Task<IActionResult> Update([FromBody] T entity)
    {
      var saveResult = await _absCrudService.SaveAsync(entity, GetUserIdentity()).ConfigureAwait(false);
      if (saveResult.IsValid) return Ok(entity);

      return BadRequest(saveResult.ToString());
    }

    [HttpDelete("id:{guid}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      var deleteResult = await _absCrudService.DeleteAsync(id, GetUserIdentity()).ConfigureAwait(false);
      if (deleteResult.IsValid) return Ok(deleteResult.Value);
      return BadRequest(deleteResult.ToString());
    }

    protected virtual void AddDefaultFilters(PagedRequest pagedRequest)
    {
      //Force 2 filters:
      //get only elements that are owned by current user
      //get only elements that are not deleted
      var owner = GetUserIdentity();
      pagedRequest.AddFilter(nameof(UserEntity.Owner), owner.Email);
      pagedRequest.AddFilter(nameof(Entity.IsDeleted), "0");
    }

    [HttpPost("Paged")]
    public virtual async Task<IActionResult> Paged([FromBody] PagedRequest request)
    {
      AddDefaultFilters(request);

      var pagedResult = await _absCrudService.PagedAsync(request).ConfigureAwait(false);
      if (pagedResult.IsValid) return Ok(pagedResult.Value);
      return BadRequest(pagedResult.ToString());
    }

    protected Owner GetUserIdentity()
    {
      if (User == null) return null;
      if (User.Identity == null) return null;
      return new Owner(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value, User.Identity.Name);
    }
  }
}