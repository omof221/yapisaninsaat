using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

    [Required, MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Email { get; set; }

     [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
