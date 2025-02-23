using CleanArchitecture.Application.Profiles;
using CleanArchitecture.Domain.Entities.User;

namespace CleanArchitecture.Application.Models.Identity;

public class GetRolesDto:ICreateMapper<Role>
{
    public string Id { get; set; }
    public string Name { get; set; }
}