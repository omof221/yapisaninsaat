using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [MaxLength(200)]
      public string? Title { get; set; }

      [MaxLength(500)]
   public string? Subtitle { get; set; }

        [MaxLength(500)]
     public string? ImageUrl { get; set; }

        [MaxLength(100)]
     public string? ButtonText { get; set; }

        [MaxLength(500)]
      public string? ButtonUrl { get; set; }

  public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
