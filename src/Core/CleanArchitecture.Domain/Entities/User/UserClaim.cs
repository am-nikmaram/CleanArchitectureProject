using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.User;

public class UserClaim:IdentityUserClaim<int>,IEntity
{
    public User User { get; set; }
}