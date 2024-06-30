using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonasCarbonTracker.Models
{
    [Table("SustainabilityMetrics")]
    public class SustainabilityMetrics
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MetricType { get; set; } = string.Empty;
        [Required]
        public decimal Value { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }

}
