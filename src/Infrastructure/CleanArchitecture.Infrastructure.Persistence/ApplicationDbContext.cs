using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Persistence.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities.User;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    //DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
            base.SavingChanges += OnSavingChanges;
        }

        private void OnSavingChanges(object sender, SavingChangesEventArgs e)
        {
            ConfigureEntityDates();
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;Integrated Security=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            //Assembly.GetExecutingAssembly()

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            //   _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            //   _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //   _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //   _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void ConfigureEntityDates()
        {
            var updatedEntities = ChangeTracker.Entries().Where(x =>
                x.Entity is ITimeModification && x.State == EntityState.Modified).Select(x => x.Entity as ITimeModification);

            var addedEntities = ChangeTracker.Entries().Where(x =>
                x.Entity is ITimeModification && x.State == EntityState.Added).Select(x => x.Entity as ITimeModification);

            foreach (var entity in updatedEntities)
            {
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                }
            }

            foreach (var entity in addedEntities)
            {
                if (entity != null)
                {
                    entity.CreatedTime = DateTime.Now;
                    entity.ModifiedDate = DateTime.Now;
                }
            }
        }
    }
}
