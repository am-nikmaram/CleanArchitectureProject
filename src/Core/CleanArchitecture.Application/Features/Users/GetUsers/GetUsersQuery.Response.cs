using CleanArchitecture.Application.Profiles;
using CleanArchitecture.Domain.Entities.User;

namespace CleanArchitecture.Application.Features.Users.GetUsers;

public record GetUsersQueryResponse : ICreateMapper<User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}