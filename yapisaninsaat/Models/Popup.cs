using System.ComponentModel.DataAnnotations;

namespace yapisaninsaat.Models
{
    public class Popup
    {
      public int Id { get; set; }

     [Required, MaxLength(200)]
       public string Title { get; set; } = string.Empty;

   public string? Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(100)]
        public string? ButtonText { get; set; }

        [MaxLength(500)]
        public string? ButtonUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(50)]
      public string ShowOnPage { get; set; } = "Home";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
