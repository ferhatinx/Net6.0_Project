using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAppBack.Core.Application.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace JwtAppBack.Infrastructure.Tools;

public static class JwtTokenGenerator
{
    public static TokenResponseDto GenerateToken(CheckUserResponseDto dto)
    {
        var claims = new List<Claim>();
        if (dto != null)
        {
            {
                claims.Add(new Claim(ClaimTypes.Role, $"{dto?.Role}"));
                var id = dto?.Id.ToString();
                if (id != null)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
                }
                if (dto?.Username != null)
                {
                    new Claim("Username", dto.Username);
                }

            };




        };


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ferhatferhatferhatferhatferhat.1"));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: "http://localhost",
            audience: "http://localhost",
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(securityToken);
        return new TokenResponseDto(token, DateTime.UtcNow.AddMinutes(1));
    }
}
