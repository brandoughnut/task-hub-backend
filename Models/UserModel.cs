namespace task_hub_backend.Models;

public class UserModel
{
    public int ID { get; set; }
    public string? Username { get; set; }
    public string? Salt { get; set; }
    public string? Hash { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Contact { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }

    public UserModel()
    {

    }
}
