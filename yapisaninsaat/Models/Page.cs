using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

     [Required, MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

  [MaxLength(200)]
        public string? MetaTitle { get; set; }

        [MaxLength(500)]
        public string? MetaDescription { get; set; }

  [MaxLength(2000)]
      public string? OldSlugs { get; set; }

        public bool IsActive { get; set; } = true;

public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }
    }
}
