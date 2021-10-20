using Microsoft.EntityFrameworkCore;
using Opravilo.DataAccess.EntityFramework.Models;

namespace Opravilo.DataAccess.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        
        public DbSet<ProjectModel> Projects { get; set; }
        
        public DbSet<ProjectPermissionModel> ProjectPermissions { get; set; }
        
        public DbSet<ProjectUserModel> ProjectUsers { get; set; }
    }
}