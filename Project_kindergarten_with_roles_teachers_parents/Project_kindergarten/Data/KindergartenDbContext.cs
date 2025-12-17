using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Project_kindergarten.Data
{
    public class KindergartenDbContext : DbContext
    {
        public DbSet<GroupData> Groups => Set<GroupData>();
        public DbSet<TeacherData> Teachers => Set<TeacherData>();
        public DbSet<ChildData> Children => Set<ChildData>();
        public DbSet<UserAccount> Users => Set<UserAccount>();

        private static string DbPath =>
            Path.Combine(AppContext.BaseDirectory, "kindergarten.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupData>(e =>
            {
                e.ToTable("Groups");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.AgeCategory).IsRequired().HasMaxLength(50);

                // поле сумісності (див. GroupData.cs)
                e.Property(x => x.Teacher).IsRequired().HasMaxLength(200);

                e.Property(x => x.Room).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<TeacherData>(e =>
            {
                e.ToTable("Teachers");
                e.HasKey(x => x.Id);

                e.Property(x => x.FullName).IsRequired().HasMaxLength(120);
                e.Property(x => x.Phone).HasMaxLength(30);
                e.Property(x => x.Email).HasMaxLength(120);
                e.Property(x => x.Position).IsRequired().HasMaxLength(60);

                e.HasOne(x => x.Group)
                    .WithMany(g => g.Teachers)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Щоб швидко знаходити основного вихователя групи
                e.HasIndex(x => new { x.GroupId, x.IsPrimary });
            });

            modelBuilder.Entity<ChildData>(e =>
            {
                e.ToTable("Children");
                e.HasKey(x => x.Id);

                e.Property(x => x.FullName).IsRequired().HasMaxLength(120);
                e.Property(x => x.ParentFullName).HasMaxLength(120);
                e.Property(x => x.ParentPhone).HasMaxLength(30);
                e.Property(x => x.Address).HasMaxLength(200);
                e.Property(x => x.MedicalNotes).HasMaxLength(300);
                e.Property(x => x.NotesForParents).HasMaxLength(500);

                e.HasOne(x => x.Group)
                    .WithMany(g => g.Children)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => x.GroupId);
            });


            modelBuilder.Entity<UserAccount>(e =>
            {
                e.ToTable("Users");
                e.HasKey(x => x.Id);

                e.Property(x => x.Username).IsRequired().HasMaxLength(60);
                e.HasIndex(x => x.Username).IsUnique();

                e.Property(x => x.PasswordHash).IsRequired();
                e.Property(x => x.PasswordSalt).IsRequired();

                e.Property(x => x.Role).IsRequired();

                e.HasOne(x => x.Teacher)
                    .WithMany()
                    .HasForeignKey(x => x.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Child)
                    .WithMany()
                    .HasForeignKey(x => x.ChildId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => x.TeacherId);
                e.HasIndex(x => x.ChildId);
            });

        }
    }
}
