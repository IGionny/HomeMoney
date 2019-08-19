namespace HomeMoney.Core.Domain
{
    public class Account : UserEntity
    {
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public decimal FirstBalance { get; set; }
    }
}