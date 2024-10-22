namespace JwtAppBack.Core.Application.Dtos;

public class CheckUserResponseDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Role { get; set; }
    
    public bool isExist { get; set; }
}
