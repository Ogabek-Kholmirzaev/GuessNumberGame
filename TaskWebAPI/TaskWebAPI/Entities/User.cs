namespace TaskWebAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;

    public virtual List<Game> Games { get; set; } = null!;
}