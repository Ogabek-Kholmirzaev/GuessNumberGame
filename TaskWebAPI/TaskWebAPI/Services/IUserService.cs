namespace TaskWebAPI.Services;

public interface IUserService
{
    Task<int> GetUserIdAsync(string userName);
}