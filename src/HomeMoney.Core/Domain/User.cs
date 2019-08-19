namespace HomeMoney.Core.Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }
}