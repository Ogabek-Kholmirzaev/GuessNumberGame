using TaskWebAPI.Entities;

namespace TaskWebAPI.Data;

public static class AppDbSeedData
{
    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

            await appDbContext.Database.EnsureCreatedAsync();

            if (!appDbContext.Users.Any())
            {
                await appDbContext.Users.AddRangeAsync(new List<User>()
                {
                    new User
                    {
                        Id = 1,
                        UserName = "test1"
                    },
                    new User
                    {
                        Id = 2,
                        UserName = "test2"
                    },
                    new User
                    {
                        Id = 3,
                        UserName = "test3"
                    },
                    new User
                    {
                        Id = 4,
                        UserName = "test4"
                    },
                });

                await appDbContext.SaveChangesAsync();
            }

            if (!appDbContext.Games.Any())
            {
                await appDbContext.Games.AddRangeAsync(new List<Game>()
                {
                    new Game
                    {
                        Id = 1,
                        SecretNumber = 1234,
                        NumberOfTries = 3,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = true,
                        UserId = 2
                    },
                    new Game
                    {
                        Id = 2,
                        SecretNumber = 2345,
                        NumberOfTries = 3,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = true,
                        UserId = 2
                    },
                    new Game
                    {
                        Id = 3,
                        SecretNumber = 3456,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 2
                    },
                    new Game
                    {
                        Id = 4,
                        SecretNumber = 1234,
                        NumberOfTries = 3,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = true,
                        UserId = 3
                    },
                    new Game
                    {
                        Id = 5,
                        SecretNumber = 2345,
                        NumberOfTries = 4,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = true,
                        UserId = 3
                    },
                    new Game
                    {
                        Id = 6,
                        SecretNumber = 3456,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 3
                    },
                    new Game
                    {
                        Id = 7,
                        SecretNumber = 8765,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 1
                    },
                    new Game
                    {
                        Id = 8,
                        SecretNumber = 9876,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 1
                    },
                    new Game
                    {
                        Id = 9,
                        SecretNumber = 2345,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 1
                    },
                    new Game
                    {
                        Id = 10,
                        SecretNumber = 1234,
                        NumberOfTries = 4,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = true,
                        UserId = 1
                    },
                    new Game
                    {
                        Id = 11,
                        SecretNumber = 1234,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 4
                    },
                    new Game
                    {
                        Id = 12,
                        SecretNumber = 2134,
                        NumberOfTries = 8,
                        MaximumTries = 8,
                        IsFinished = true,
                        IsWinner = false,
                        UserId = 4
                    }
                });
            }

            await appDbContext.SaveChangesAsync();
            
        }
    }
}