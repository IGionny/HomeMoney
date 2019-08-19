using System;

namespace HomeMoney.Core.Domain
{
    public class AccountBalance : Entity
    {   
        public EntityReference Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime BalanceAt { get; set; }
    }
}