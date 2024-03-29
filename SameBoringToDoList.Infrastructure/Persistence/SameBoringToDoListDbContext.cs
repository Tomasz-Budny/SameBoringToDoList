﻿using Microsoft.EntityFrameworkCore;
using SameBoringToDoList.Domain.Entities;
using SameBoringToDoList.Infrastructure.Persistence.Configurations;

namespace SameBoringToDoList.Infrastructure.Persistence
{
    public class SameBoringToDoListDbContext : DbContext
    {
        public SameBoringToDoListDbContext(DbContextOptions<SameBoringToDoListDbContext> options) : base(options) { }

        public DbSet<ToDoList> ToDoLists { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var toDoListConfiguration = new ToDoListConfiguration();
            modelBuilder.ApplyConfiguration(toDoListConfiguration);

            var userConfiguration = new UserConfiguration();
            modelBuilder.ApplyConfiguration(userConfiguration);
        }
    }
}
