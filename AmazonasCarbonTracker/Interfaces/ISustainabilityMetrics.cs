using AmazonasCarbonTracker.Dtos.SustainabilityMetricsDto;
using AmazonasCarbonTracker.Models;

namespace AmazonasCarbonTracker.Interfaces;

public interface ISustainabilityMetrics
{
    Task<List<SustainabilityMetrics>> GetAllAsync();
    Task<SustainabilityMetrics?> GetByIdAsync(int id);
    Task<SustainabilityMetrics> CreateAsync(SustainabilityMetrics sustainabilityMetricsDto);
    Task<SustainabilityMetrics?> DeleteAsync(int id);
    Task<SustainabilityMetrics> UpdateAsync(int id, UpdateSustainabilityMetricsDto sustainabilityMetricsDto);
}
