using AmazonasCarbonTracker.Dtos;
using AmazonasCarbonTracker.Models;

namespace AmazonasCarbonTracker.Interfaces;

public interface IEmissionRecordRepo
{
    Task<List<EmissionRecord>> GetAllAsync();
    Task<EmissionRecord?> GetByIdAsync(int id);
    Task<EmissionRecord> CreateAsync(EmissionRecord emissionRecord);
    Task<EmissionRecord?> DeleteAsync(int id);
    Task<EmissionRecord> UpdateAsync(int id, UpdateEmissionDto emissionDto);
}
