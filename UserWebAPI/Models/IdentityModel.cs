using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserWebAPI.Entities;

namespace UserWebAPI.Models
{
    /*public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }*/

    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(u =>
            {
                u.ToTable("User");
                u.HasKey(x => x.Id);
            });
            builder.Entity<Role>(u =>
            {
                u.ToTable("Role");
                u.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserRole<string>>(u =>
            {
                u.ToTable("UserRole");
                u.HasKey(x => new { x.RoleId, x.UserId });
            });
            builder.Entity<IdentityUserClaim<string>>(u =>
            {
                u.ToTable("UserClaim");
                u.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserLogin<string>>(u =>
            {
                u.ToTable("UserLogin");
                u.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
        }
    }
}
