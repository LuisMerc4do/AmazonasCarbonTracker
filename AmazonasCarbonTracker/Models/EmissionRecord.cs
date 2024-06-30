using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonasCarbonTracker.Models
{
    [Table("EmissionRecord")]
    public class EmissionRecord
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string ActivityType { get; set; } = string.Empty;
        [Required]
        public double EmissionAmount { get; set; }
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
