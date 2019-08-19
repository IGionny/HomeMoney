using System;

namespace HomeMoney.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class UserEntity : Entity
    {
        public EntityReference User { get; set; }
    }

    public class EntityReference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}