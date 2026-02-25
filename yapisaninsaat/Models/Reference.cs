using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Reference
{
        public int Id { get; set; }

     [Required, MaxLength(200)]
      public string CompanyName { get; set; } = string.Empty;

    [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [MaxLength(500)]
    public string? WebsiteUrl { get; set; }

        public int Order { get; set; }

    public bool IsActive { get; set; } = true;
    }
}
