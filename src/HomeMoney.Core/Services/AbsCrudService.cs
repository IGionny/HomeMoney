using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMoney.Core.Domain;
using HomeMoney.Core.Models;

namespace HomeMoney.Core.Services
{
    public abstract class AbsCrudService<T> where T : UserEntity
    {
        //Temporary implementation  in memory
        public IDictionary<Guid, T> InMemoryEntities { get; set; } = new Dictionary<Guid, T>();


        public virtual async Task<ResultModel<T>> ValidateAsync(T entity)
        {
            return new ResultModel<T>();
        }

        public virtual async Task<ResultModel<T>> SaveAsync(T entity, Owner author)
        {
            var result = new ResultModel<T>(entity);
            if (entity.IsDeleted)
            {
                result.AddError("Can't update an entity that is deleted");
                return result;
            }
            if (entity.Id.Equals(Guid.Empty))
            {
                entity.Owner = author;
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = author.Name;
            }
            else
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = author.Name;
            }

            if (entity.Id.Equals(Guid.Empty))
            {
                entity.Id = Guid.NewGuid();
                InMemoryEntities.Add(entity.Id, entity);
            }
            else
            {
                InMemoryEntities[entity.Id] = entity;
            }

            return result;
        }

        public virtual async Task<ResultModel<T>> GetAsync(Guid entityId)
        {
            var result = new ResultModel<T>();

            if (InMemoryEntities.ContainsKey(entityId))
            {
                result.Value = InMemoryEntities[entityId];
            }

            if (result.Value.IsDeleted)
            {
                result.AddInfo("The entity is deleted");
            }

            return result;
        }

        public virtual async Task<PagedResponse<T>> PagedAsync(PagedRequest request)
        {
            var result = new PagedResponse<T>();
            result.Current = request.Page;
            result.PageSize = request.PageSize;
            result.Total = InMemoryEntities.Count;
            result.Value = InMemoryEntities.Select(x => x.Value).Where(x => !x.IsDeleted)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize).ToList();
            return result;
        }

        public virtual async Task<ResultModel<T>> DeleteAsync(Guid entityId, Owner author)
        {
            var result = new ResultModel<T>();

            if (InMemoryEntities.ContainsKey(entityId))
            {
                //Soft delete
                InMemoryEntities[entityId].IsDeleted = true;
                InMemoryEntities[entityId].UpdatedAt = DateTime.UtcNow;
                InMemoryEntities[entityId].UpdatedBy = author.Name;
                InMemoryEntities.Remove(entityId);
            }

            return result;
        }
    }
}