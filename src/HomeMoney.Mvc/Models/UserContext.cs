namespace HomeMoney.Mvc.Models
{
  public class UserContext
  {
    public UserContext()
    {
      
    }

    public UserContext(string name, string email)
    {
      Name = name;
      Email = email;
    }
    public string Name { get; set; }
    public string Email { get; set; }
  }
}