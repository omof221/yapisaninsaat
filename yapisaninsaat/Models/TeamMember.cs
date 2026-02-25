using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class TeamMember
  {
     public int Id { get; set; }

        [Required, MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

        [MaxLength(200)]
     public string? Title { get; set; }

     [MaxLength(500)]
        public string? Specialty { get; set; }

    [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public int Order { get; set; }

      public bool IsActive { get; set; } = true;
    }
}
