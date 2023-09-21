using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure.Persistence.Contexts
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.TaskId);
        }

        public DbSet<TaskItem> Tasks { get; set;}

        public DbSet<User> Users { get; set; }
             
    }
}
