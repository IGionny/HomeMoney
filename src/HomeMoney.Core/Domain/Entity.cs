using System;
using System.Text;

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
        public Owner Owner { get; set; }
    }


    public class Owner
    {
        public Owner()
        {
        }

        public Owner(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class EntityReference
    {
        public EntityReference()
        {
        }

        public EntityReference(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}