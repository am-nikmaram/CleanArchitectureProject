using CleanArchitecture.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Identity.Identity.Store;

public class RoleStore:RoleStore<Role,ApplicationDbContext,int,UserRole,RoleClaim>
{
    public RoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}