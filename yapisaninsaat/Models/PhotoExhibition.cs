using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class PhotoExhibition
    {
        public int Id { get; set; }

      [MaxLength(200)]
        public string? Title { get; set; }

  [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [MaxLength(100)]
      public string? Category { get; set; }

    public bool IsHomeActive { get; set; }

        public int Order { get; set; }
    }
}
