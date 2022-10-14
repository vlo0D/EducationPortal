using EducationPortal.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DAL.DataContext
{
    public class PortalContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {
        }

        public PortalContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<ArticleMaterial> Articles { get; set; }

        public DbSet<BookMaterial> Books { get; set; }

        public DbSet<VideoMaterial> Videos { get; set; }

        public DbSet<Skill> Skills { get; set; }
    }
}
