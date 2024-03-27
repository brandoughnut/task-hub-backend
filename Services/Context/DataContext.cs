using Microsoft.EntityFrameworkCore;
using task_hub_backend.Models;

namespace task_hub_backend.Services.Context;

    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
