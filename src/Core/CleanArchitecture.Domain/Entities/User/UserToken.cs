using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.User;

public class UserToken:IdentityUserToken<int>,IEntity
{
    public UserToken()
    {
        GeneratedTime=DateTime.Now;
    }

    public User User { get; set; }
    public DateTime GeneratedTime { get; set; }
}