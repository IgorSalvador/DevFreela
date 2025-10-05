using System.Runtime.CompilerServices;
using DevFreela.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Skill>(x =>
        {
            x.HasKey(s => s.Id);
        });

        builder.Entity<UserSkill>(x =>
        {
            x.HasKey(us => us.Id);

            x.HasOne(u => u.Skill)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(s => s.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProjectComment>(x =>
        {
            x.HasKey(p => p.Id);

            x.HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<User>(x =>
        {
            x.HasKey(u => u.Id);

            x.HasMany(u => u.Skills)
                .WithOne(u => u.User)
                .HasForeignKey(us => us.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Project>(x =>
        {
            x.HasKey(p => p.Id);

            x.HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(p => p.Client)
                .WithMany(o => o.OwnedProjects)
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }
}