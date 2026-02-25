using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Setting
    {
        public int Id { get; set; }

        // Genel
        [MaxLength(200)]
        public string? CompanyName { get; set; }

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [MaxLength(500)]
        public string? FaviconUrl { get; set; }

        [MaxLength(1000)]
        public string? FooterText { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        public string? HeaderScript { get; set; }

        public string? FooterScript { get; set; }

        // SMTP
        [MaxLength(200)]
        public string? SmtpServer { get; set; }

        public int? SmtpPort { get; set; }

        [MaxLength(200)]
        public string? SmtpEmail { get; set; }

        [MaxLength(200)]
        public string? SmtpPassword { get; set; }

        // Hakkımızda
        [MaxLength(300)]
        public string? AboutTitle { get; set; }

        [MaxLength(1000)]
        public string? AboutSubtitle { get; set; }

        public string? AboutContent { get; set; }

        [MaxLength(500)]
        public string? AboutImageUrl { get; set; }

        [MaxLength(100)]
        public string? FoundedYear { get; set; }

        [MaxLength(300)]
        public string? Expertise { get; set; }

        [MaxLength(300)]
        public string? DeliveryModel { get; set; }

        // Sosyal Medya
        [MaxLength(500)]
        public string? LinkedInUrl { get; set; }

        [MaxLength(500)]
        public string? InstagramUrl { get; set; }

        [MaxLength(500)]
        public string? TwitterUrl { get; set; }
    }
}
