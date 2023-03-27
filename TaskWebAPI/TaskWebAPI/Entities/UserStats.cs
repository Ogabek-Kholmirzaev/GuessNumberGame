namespace TaskWebAPI.Entities;

public class UserStats
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int TotalWins { get; set; }
    public int TotalGames { get; set; }
    public int TotalTries { get; set; }
    public double SuccessRate { get; set; }
}