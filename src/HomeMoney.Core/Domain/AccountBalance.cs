using System;

namespace HomeMoney.Core.Domain
{
    public class AccountBalance : UserEntity
    {   
        public EntityReference Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime BalanceAt { get; set; }
    }
}