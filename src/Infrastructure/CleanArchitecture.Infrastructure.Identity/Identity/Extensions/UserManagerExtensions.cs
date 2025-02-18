using CleanArchitecture.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<string> GenerateRefreshTokenAsync(this UserManager<User> userManager, User user, bool rememberMe)
        {
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(7);

            await userManager.UpdateAsync(user);
            return refreshToken;
        }

        public static async Task<bool> ValidateRefreshTokenAsync(this UserManager<User> userManager, string refreshToken)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.Now);
            return user != null;
        }

        public static async Task<User> GetUserByRefreshTokenAsync(this UserManager<User> userManager, string refreshToken)
        {
            return await userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.Now);
        }
    }
}
