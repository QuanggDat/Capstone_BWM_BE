﻿using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.DataAccess
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<Area> Area { get; set; }
        //public DbSet<Floor> Floor { get; set; }
        //public DbSet<Team> Team { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemMaterial> ItemMaterial { get; set; }
        public DbSet<LeaderTask> LeaderTask { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialCategory> MaterialCategory { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderDetailMaterial> OrderDetailMaterial { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<ProcedureItem> ProcedureItem { get; set; }
        public DbSet<ProcedureStep> ProcedureStep { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<Supply> Supply { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<WorkerTask> WorkerTask { get; set; }
        public DbSet<WorkerTaskDetail> WorkerTaskDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

            // Configure the relationship between OrdersAssignTo and User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.AssignTo)
                .WithMany(u => u.OrdersAssignTo)
                .HasForeignKey(o => o.assignToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between OrdersCreatedBy and User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedBy)
                .WithMany(u => u.OrdersCreatedBy)
                .HasForeignKey(o => o.createdById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
