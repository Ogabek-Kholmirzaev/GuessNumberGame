using Microsoft.EntityFrameworkCore;
using TaskWebAPI.Data;
using TaskWebAPI.Entities;

namespace TaskWebAPI.Services;

public class UserService : IUserService
{
    private readonly AppDbContext appDbContext;

    public UserService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> GetUserIdAsync(string userName)
    {
        var user = await this.appDbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        if(user != null)
            return user.Id;

        var newUser = new User
        {
            UserName = userName,
            Games = new List<Game>()
        };

        await this.appDbContext.Users.AddAsync(newUser);
        await this.appDbContext.SaveChangesAsync();

        return newUser.Id;
    }
}