using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Service
    {
      public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ShortDescription { get; set; }

    public string? Description { get; set; }

        [MaxLength(100)]
        public string? Icon { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

     public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
