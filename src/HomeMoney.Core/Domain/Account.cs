using System;
using Microsoft.AspNetCore.Identity;

namespace HomeMoney.Core.Domain
{
    public class Account : UserEntity
    {
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public decimal FirstBalance { get; set; }
        public DateTime? FromAt { get; set; }
        public DateTime? ToAt { get; set; }
    }
    
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser
    {
    }

    public class ApplicationRole
    {
        
    }
}