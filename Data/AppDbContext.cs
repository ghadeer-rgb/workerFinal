using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using workerFinal;
using workerFinal.Models;


namespace workerFinal.Data
{
    public class AppDbContext:DbContext
    {
//protected override void OnConfiguring(DbContextOptionsBuilder options) {
//            options.UseSqlServer(Connections.sqlConStr);



            public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        
        public DbSet<Job> jobs { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<TaskerProfile> taskerProfiles { get; set; }
        public DbSet<Review> reviews { get; set; }

        public DbSet<Favorite> favorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // USER
            // ============================

            modelBuilder.Entity<User>()
                .HasKey(u => u.userId);

            modelBuilder.Entity<User>()
                .Property(u => u.email)
                .IsRequired();

            // ============================
            // TASKER PROFILE (One-to-One)
            // ============================

            modelBuilder.Entity<TaskerProfile>()
                .HasKey(tp => tp.taskerId);

            modelBuilder.Entity<TaskerProfile>()
                .HasOne(tp => tp.tasker)
                .WithOne(u => u.taskerProfile)
                .HasForeignKey<TaskerProfile>(tp => tp.taskerId)
                .OnDelete(DeleteBehavior.Cascade);



            // ============================
            // JOB (Customer -> Tasker)
            // ============================

            modelBuilder.Entity<Job>()
                .HasKey(j => j.taskId);

            // Customer creates many jobs
            modelBuilder.Entity<Job>()
                .HasOne(j => j.customer)
                .WithMany(u => u.customerTasks)
                .HasForeignKey(j => j.customerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Tasker gets many assigned jobs
            modelBuilder.Entity<Job>()
                .HasOne(j => j.tasker)
                .WithMany(u => u.assignedTasks)
                .HasForeignKey(j => j.taskerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Job belongs to one Service
            modelBuilder.Entity<Job>()
                .HasOne(j => j.service)
                .WithMany(s => s.tasks)
                .HasForeignKey(j => j.serviceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // REVIEW
            // ============================

            modelBuilder.Entity<Review>()
                .HasKey(r => r.reviewId);

            // Customer gives many reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.customer)
                .WithMany(u => u.reviewsGiven)
                .HasForeignKey(r => r.customerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tasker receives many reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.tasker)
                .WithMany(u => u.reviewsReceived)
                .HasForeignKey(r => r.taskerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review belongs to one Job (1-to-1)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.task)
                .WithOne(j => j.Review)
                .HasForeignKey<Review>(r => r.taskId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


            // TaskerProfile ↔ Service (Many-to-One)
            // TaskerProfile belongs to one Service
            modelBuilder.Entity<TaskerProfile>()
                .HasOne(tp => tp.service)
                .WithMany(s => s.taskerProfiles)
                .HasForeignKey(tp => tp.serviceId)
                .OnDelete(DeleteBehavior.Restrict);


 modelBuilder.Entity<Favorite>()
    .HasKey(f => f.id);

        modelBuilder.Entity<Favorite>()
    .HasOne(f => f.user)
    .WithMany(u => u.Favorites)//customer
    .HasForeignKey(f => f.userId)
    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Favorite>()
    .HasOne(f => f.tasker)
    .WithMany(u=>u.FavoritedBy)//tasker
    .HasForeignKey(f => f.taskerId)
    .OnDelete(DeleteBehavior.Restrict);

    }

    }
}
