using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos;

public class UpdateEmissionDto
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    public string ActivityType { get; set; } = string.Empty;
    [Required]
    public double EmissionAmount { get; set; }
}
