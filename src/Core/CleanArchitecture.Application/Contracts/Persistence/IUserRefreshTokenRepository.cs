﻿using CleanArchitecture.Domain.Entities.User;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IUserRefreshTokenRepository
{
    Task<Guid> CreateToken(int userId);
    Task<UserRefreshToken> GetTokenWithInvalidation(Guid id);
    Task<User> GetUserByRefreshToken(Guid tokenId);
    Task RemoveUserOldTokens(int userId, CancellationToken cancellationToken);
}