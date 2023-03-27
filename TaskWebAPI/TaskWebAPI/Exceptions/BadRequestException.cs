namespace TaskWebAPI.Exceptions;

public class BadRequestException : Exception
{
    public int ErrorCode { get; set; } = 400;

    public BadRequestException(string message) : base(message) { }
}