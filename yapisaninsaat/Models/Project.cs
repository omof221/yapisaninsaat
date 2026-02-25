using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yapisaninsaat.Models
{
    public class Project
  {
        public int Id { get; set; }

        public int CategoryId { get; set; }

[ForeignKey("CategoryId")]
    public ProjectCategory? Category { get; set; }

        [Required, MaxLength(200)]
      public string Title { get; set; } = string.Empty;

   [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

        [MaxLength(500)]
  public string? ShortDescription { get; set; }

   public string? Description { get; set; }

    [MaxLength(300)]
        public string? Location { get; set; }

    public DateTime? StartDate { get; set; }

      public DateTime? EndDate { get; set; }

        /// <summary>
    /// 0: Devam Eden, 1: Biten
        /// </summary>
        public int ProjectStatus { get; set; }

    public bool IsFeatured { get; set; }

        [MaxLength(2000)]
     public string? OldSlugs { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

public ICollection<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();
    }
}
