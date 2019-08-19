using System;
using System.Collections.Generic;
using System.Linq;
using HomeMoney.Core.Domain.Enums;

namespace HomeMoney.Core.Domain
{
    public class Transaction : UserEntity
    {
        public string Title { get; set; }
        public decimal AmountExpense { get; set; }
        public decimal AmountIncome { get; set; }
        public TransactionType Type { get; set; }
        public string Notes { get; set; }
        public DateTime ExecutedAt { get; set; }
        public DateTime? ValueDate { get; set; }
        public EntityReference Category { get; set; }

        public EntityReference AccountFrom { get; set; }
        public EntityReference AccountTo { get; set; }

        public string Tags { get; protected set; }

        public IList<string> TagList
        {
            get => Tags.Split(',');
            set
            {
                if (value == null || !value.Any())
                {
                    Tags = null;
                    return;
                }
                
                Tags = string.Join( ",", new HashSet<string>(value.Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))));
            }
        }
    }
}