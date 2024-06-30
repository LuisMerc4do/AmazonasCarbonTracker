using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos.SustainabilityMetricsDto
{
    public class CreateSustainabilityMetricsDto
    {
        [Required(ErrorMessage = "Metric type is required.")]
        public string MetricType { get; set; }

        [Required(ErrorMessage = "Metric value is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Metric value must be a non-negative number.")]
        public decimal MetricValue { get; set; }
        public string? AppUserId { get; set; }
    }
}
