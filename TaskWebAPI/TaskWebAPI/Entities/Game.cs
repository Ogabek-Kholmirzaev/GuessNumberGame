namespace TaskWebAPI.Entities;

public class Game
{
    public int Id { get; set; }
    public int SecretNumber { get; set; }
    public int NumberOfTries { get; set; }
    public int MaximumTries { get; set; }
    public bool IsFinished { get; set; }
    public bool IsWinner { get; set; }

    public virtual User User { get; set; } = null!;
    public int UserId { get; set; }
}