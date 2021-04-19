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
    }
}