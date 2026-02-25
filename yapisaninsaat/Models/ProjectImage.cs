using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yapisaninsaat.Models
{
    public class ProjectImage
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public int Order { get; set; }

        public bool IsCover { get; set; }
    }
}
