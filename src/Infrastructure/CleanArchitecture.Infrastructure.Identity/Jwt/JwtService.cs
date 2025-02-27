﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.Domain.Entities.User;
using CleanArchitecture.Infrastructure.Identity.Dtos;
using CleanArchitecture.Infrastructure.Identity.Extensions;
using CleanArchitecture.Infrastructure.Identity.Identity.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Identity.Jwt;

public class JwtService : IJwtService
{
    private readonly IdentitySettings _siteSetting;
    private readonly AppUserManager _userManager;
    private IUserClaimsPrincipalFactory<User> _claimsPrincipal;

    private readonly IApplicationDbContext _applicationDbContext;
  //  private readonly IUnitOfWork _unitOfWork;
    //private readonly AppUserClaimsPrincipleFactory claimsPrincipleFactory;

    public JwtService(IOptions<IdentitySettings> siteSetting, AppUserManager userManager, IUserClaimsPrincipalFactory<User> claimsPrincipal, IApplicationDbContext applicationDbContext)
    {
        _siteSetting = siteSetting.Value;
        _userManager = userManager;
        _claimsPrincipal = claimsPrincipal;
        //      _unitOfWork = unitOfWork;
        _applicationDbContext=applicationDbContext;
    }
    public async Task<AccessToken> GenerateAsync(User user, bool rememberMe = false)
    {
        var secretKey = Encoding.UTF8.GetBytes(_siteSetting.SecretKey); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.Encryptkey); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


        var claims = await _getClaimsAsync(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _siteSetting.Issuer,
            Audience = _siteSetting.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(0),
            Expires = DateTime.Now.AddMinutes(_siteSetting.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);


        //var refreshToken = await _unitOfWork.UserRefreshTokenRepository.CreateToken(user.Id);
        //await _unitOfWork.CommitAsync();

        var token = new UserRefreshToken { IsValid = true, UserId = user.Id };
        _applicationDbContext.UserRefreshTokens.Add(token);
        _applicationDbContext.SaveChanges();
        
        

        // Generate refresh token using the extension method
      //  var refreshToken = await _userManager.GenerateRefreshTokenAsync(user, rememberMe);


        return new AccessToken(securityToken,token.Id.ToString());
    }

    public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.SecretKey)),
            ValidateLifetime = false,
            TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.Encryptkey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return Task.FromResult(principal);
    }

    public async Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        var result = await this.GenerateAsync(user);
        return result;
    }




    #region added byAI

    public async Task<AccessToken> RefreshTokenAsync(Guid refreshToken)
    {

        var refreshtoken =await _applicationDbContext.UserRefreshTokens.Where(t => t.IsValid && t.Id.Equals(refreshToken)).FirstOrDefaultAsync();


        if (refreshtoken == null)
        {
            return null; // Invalid refresh token
        }

        refreshtoken.IsValid = false;
        _applicationDbContext.UserRefreshTokens.Update(refreshtoken);

        var user = await _applicationDbContext.UserRefreshTokens.Include(t => t.User).Where(c => c.Id.Equals(refreshToken))
          .Select(c => c.User).FirstOrDefaultAsync();
        if (user is null)
            return null;

        return await GenerateAsync(user, true); // Pass true to extend the session
    }

    #endregion
    private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
    {
        var result = await _claimsPrincipal.CreateAsync(user);
        return result.Claims;
    }
}