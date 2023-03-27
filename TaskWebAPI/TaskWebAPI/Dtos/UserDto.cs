using System.ComponentModel.DataAnnotations;

namespace TaskWebAPI.Dtos;

public class UserDto
{
    [Required]
    public string? UserName { get; set; }
}