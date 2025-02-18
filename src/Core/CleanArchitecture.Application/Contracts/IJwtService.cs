using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.Domain.Entities.User;
using System.Security.Claims;

namespace CleanArchitecture.Application.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user, bool rememberMe = false);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber);
    // Task<AccessToken> RefreshToken(Guid refreshTokenId);
    Task<AccessToken> RefreshTokenAsync(string refreshToken);
}