using TaskWebAPI.Dtos;
using TaskWebAPI.Entities;

namespace TaskWebAPI.Services;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(int gameId);
    Task<GameDto> CreateGameAsync(string userName);
    Task<GuessResponse> GuessNumberAsync(GuessRequest guessRequest);
    Task<List<GameDto>> GetUserGamesAsync(string userName);
    Task<List<UserStats>> GetLeaderboardAsync();
}