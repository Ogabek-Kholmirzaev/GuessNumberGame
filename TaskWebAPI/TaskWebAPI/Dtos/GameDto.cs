using TaskWebAPI.Entities;

namespace TaskWebAPI.Dtos;

public class GameDto
{
    public int Id { get; set; }
    public int SecretNumber { get; set; }
    public int NumberOfTries { get; set; }
    public int MaximumTries { get; set; }
    public bool IsFinished { get; set; }
    public bool IsWinner { get; set; }
    public int UserId { get; set; }
}