using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class FAQ
    {
     public int Id { get; set; }

        [Required]
        public string Question { get; set; } = string.Empty;

        [Required]
        public string Answer { get; set; } = string.Empty;

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
