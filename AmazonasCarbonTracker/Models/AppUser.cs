using Microsoft.AspNetCore.Identity;

namespace AmazonasCarbonTracker.Models;

public class AppUser : IdentityUser
{
    public List<EmissionRecord> EmissionRecords { get; set; } = new List<EmissionRecord>();
    public List<SustainabilityMetrics> SustainabilityMetrics { get; set; } = new List<SustainabilityMetrics>();
}
