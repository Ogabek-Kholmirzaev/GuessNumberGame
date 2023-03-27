using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWebAPI.Dtos;
using TaskWebAPI.Entities;
using TaskWebAPI.Services;

namespace TaskWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService gamesService;

    public GamesController(IGameService gamesService)
    {
        this.gamesService = gamesService;
    }

    [HttpGet("game/{gameId:int}")]
    public async Task<IActionResult> GetGameById(int gameId) => 
        Ok(await this.gamesService.GetGameByIdAsync(gameId));


    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard() =>
        Ok(await this.gamesService.GetLeaderboardAsync());

    [HttpGet("user-games")]
    public async Task<IActionResult> GetUserGames([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(await this.gamesService.GetUserGamesAsync(userDto.UserName!));
    }

    [HttpPost("new-game")]
    public async Task<IActionResult> CreateGame([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(await this.gamesService.CreateGameAsync(userDto.UserName!));
    }

    [HttpPost("guess-number")]
    public async Task<IActionResult> GuessNumber([FromBody] GuessRequest guessRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var guessResponse = await this.gamesService.GuessNumberAsync(guessRequest);

        return Ok(guessResponse);
    }
}