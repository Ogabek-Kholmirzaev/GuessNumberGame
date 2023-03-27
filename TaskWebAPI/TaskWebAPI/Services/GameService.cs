using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskWebAPI.Data;
using TaskWebAPI.Dtos;
using TaskWebAPI.Entities;
using TaskWebAPI.Exceptions;

namespace TaskWebAPI.Services;

public class GameService : IGameService
{
    private readonly IUserService userService;
    private readonly AppDbContext appDbContext;

    public GameService(IUserService userService, AppDbContext appDbContext)
    {
        this.userService = userService;
        this.appDbContext = appDbContext;
    }

    public async Task<GameDto> GetGameByIdAsync(int gameId)
    {
        var game = await this.appDbContext.Games.FirstOrDefaultAsync(game => game.Id == gameId);

        if (game == null)
            throw new BadRequestException("Game does not exists!");

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto> CreateGameAsync(string userName)
    {
        var userId = await this.userService.GetUserIdAsync(userName);
        var secretNumber = CalculationService.GenerateSecretNumber();

        var newGame = new Game
        {
            SecretNumber = secretNumber,
            NumberOfTries = 0,
            MaximumTries = 8,
            IsFinished = false,
            IsWinner = false,
            UserId = userId
        };

        await this.appDbContext.Games.AddAsync(newGame);
        await this.appDbContext.SaveChangesAsync();

        var newGameDto = newGame.Adapt<GameDto>();

        return newGameDto;
    }

    public async Task<GuessResponse> GuessNumberAsync(GuessRequest guessRequest)
    {
        var game = await this.appDbContext.Games.FirstOrDefaultAsync(game => game.Id == guessRequest.GameId);
        var userId = await this.userService.GetUserIdAsync(guessRequest.UserName!);

        if (game == null)
            throw new BadRequestException("Game does not exists!");

        if (userId != game.UserId)
            throw new BadRequestException("This is not your game!");

        if (game.IsFinished || game.NumberOfTries > game.MaximumTries)
            throw new BadRequestException("You cannot try anymore!");

        if (game.SecretNumber == guessRequest.GuessNumber)
        {
            game.IsFinished = true;
            game.IsWinner = true;
        }

        game.NumberOfTries++;

        if (game.NumberOfTries >= game.MaximumTries)
            game.IsFinished = true;

        await this.appDbContext.SaveChangesAsync();

        var message = CalculationService.CheckGuessNumber(game.SecretNumber, guessRequest.GuessNumber!.Value);
        var guessResponse = new GuessResponse
        {
            Message = message,
            IsFinished = game.IsFinished,
            IsWinner = game.IsWinner,
            SecretNumber = game.SecretNumber,
            NumberOfTries = game.MaximumTries - game.NumberOfTries
        };

        return guessResponse;
    }

    public async Task<List<GameDto>> GetUserGamesAsync(string userName)
    {
        var user = await this.appDbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        if (user == null)
            throw new BadRequestException("User does not exists!");

        var userGames = user.Games;
        var userGamesDto = userGames.Adapt<List<GameDto>>();

        return userGamesDto;
    }

    public async Task<List<UserStats>> GetLeaderboardAsync()
    {
        var users = await this.appDbContext.Users.ToListAsync();
        var usersStats = new List<UserStats>();

        foreach (var user in users)
        {
            var totalWins = 0;
            var totalGames = 0;
            var totalTries = 0;

            foreach (var userGame in user.Games)
            {
                if(userGame.IsWinner)
                    totalWins++;

                totalGames++;
                totalTries = userGame.NumberOfTries > 8 ? totalTries + 8 : totalTries + userGame.NumberOfTries;
            }

            usersStats.Add(new UserStats
            {
                Id = 0,
                UserName = user.UserName,
                TotalWins = totalWins,
                TotalGames = totalGames,
                TotalTries = totalTries,
                SuccessRate = totalGames == 0 ? 0 : Math.Round((double)totalWins / totalGames * 100, 1)
            });
        }

        var id = 1;
        var sortedUsersStats = usersStats
            .OrderByDescending(userStats => userStats.SuccessRate)
            .ThenBy(userStats => userStats.TotalTries)
            .ToList();

        foreach (var sortedUsersStat in sortedUsersStats)
        {
            sortedUsersStat.Id = id++;
        }

        return sortedUsersStats;
    }
}