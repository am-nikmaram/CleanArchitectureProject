
using CleanArchitecture.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Contracts;

    public interface IApplicationDbContext
    {
         public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
         
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
