using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Statistic
    {
        public int Id { get; set; }

      [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Value { get; set; } = string.Empty;

        [MaxLength(100)]
     public string? Icon { get; set; }

        public int Order { get; set; }

    public bool IsActive { get; set; } = true;
  }
}
