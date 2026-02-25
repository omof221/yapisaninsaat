using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

    [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

      [MaxLength(200)]
  public string? Title { get; set; }

  [Required]
   public string Comment { get; set; } = string.Empty;

     [MaxLength(500)]
      public string? ImageUrl { get; set; }

        public int Order { get; set; }

    public bool IsActive { get; set; } = true;
    }
}
