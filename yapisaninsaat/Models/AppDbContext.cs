using Microsoft.EntityFrameworkCore;

namespace yapisaninsaat.Models
{
    public class AppDbContext : DbContext
    {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Page> Pages { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ProjectCategory> ProjectCategories { get; set; } = null!;
  public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectImage> ProjectImages { get; set; } = null!;
        public DbSet<PhotoExhibition> PhotoExhibitions { get; set; } = null!;
        public DbSet<Popup> Popups { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
      public DbSet<Statistic> Statistics { get; set; } = null!;
public DbSet<Testimonial> Testimonials { get; set; } = null!;
  public DbSet<Reference> References { get; set; } = null!;
        public DbSet<FAQ> FAQs { get; set; } = null!;
        public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TeamMember> TeamMembers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Project>()
                .HasOne(p => p.Category)
         .WithMany(c => c.Projects)
          .HasForeignKey(p => p.CategoryId)
       .OnDelete(DeleteBehavior.Restrict);

modelBuilder.Entity<ProjectImage>()
        .HasOne(pi => pi.Project)
                .WithMany(p => p.ProjectImages)
                .HasForeignKey(pi => pi.ProjectId)
    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Page>().HasIndex(p => p.Slug).IsUnique();
            modelBuilder.Entity<Service>().HasIndex(s => s.Slug).IsUnique();
            modelBuilder.Entity<ProjectCategory>().HasIndex(c => c.Slug).IsUnique();
    modelBuilder.Entity<Project>().HasIndex(p => p.Slug).IsUnique();
        }
    }
}
