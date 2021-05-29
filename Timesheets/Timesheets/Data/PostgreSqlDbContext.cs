using Microsoft.EntityFrameworkCore;
using Timesheets.Models;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Data.Ef
{
    public class PostgreSqlDbContext : DbContext
    {
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JwtRefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Employee>().ToTable("employees");

            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.User)
                .WithMany(user => user.Employees)
                .HasForeignKey("UserId");

            modelBuilder.Entity<JwtRefreshToken>().ToTable("tokens");
        }
    }
}