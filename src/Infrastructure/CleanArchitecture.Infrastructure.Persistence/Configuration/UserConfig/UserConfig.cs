using CleanArchitecture.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configuration.UserConfig;

internal class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users","usr").Property(p => p.Id).HasColumnName("UserId");
        builder.Property(p => p.FamilyName).IsRequired(false);
        builder.Property(p => p.Name).IsRequired(false);
       
     
    }
}