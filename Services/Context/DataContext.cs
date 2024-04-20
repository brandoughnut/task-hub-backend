using Microsoft.EntityFrameworkCore;
using task_hub_backend.Models;

namespace task_hub_backend.Services.Context;

    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<ProjectModel> ProjectInfo { get; set; }
        public DbSet<RelationModel> RelationInfo { get; set; }
        public DbSet<TaskModel> TaskInfo { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
