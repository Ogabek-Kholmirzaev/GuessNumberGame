namespace TaskWebAPI.Entities;

public class GuessResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsFinished { get; set; }
    public bool IsWinner { get; set; }
    public int SecretNumber { get; set; }
    public int NumberOfTries { get; set; }
}