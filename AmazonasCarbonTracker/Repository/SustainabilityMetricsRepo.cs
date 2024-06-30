using AmazonasCarbonTracker.Data;
using AmazonasCarbonTracker.Dtos.SustainabilityMetricsDto;
using AmazonasCarbonTracker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonasCarbonTracker.Repository
{
    public class SustainabilityMetricsRepo : ISustainabilityMetrics
    {
        private readonly ApplicationDBContext _context;

        public SustainabilityMetricsRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Models.SustainabilityMetrics> CreateAsync(Models.SustainabilityMetrics sustainabilityMetricsDto)
        {
            await _context.SustainabilityMetrics.AddAsync(sustainabilityMetricsDto);
            await _context.SaveChangesAsync();
            return sustainabilityMetricsDto;
        }

        public async Task<Models.SustainabilityMetrics?> DeleteAsync(int id)
        {
            var metricModel = await _context.SustainabilityMetrics.FirstOrDefaultAsync(x => x.Id == id);
            if (metricModel == null)
            {
                return null;
            }
            _context.SustainabilityMetrics.Remove(metricModel);
            await _context.SaveChangesAsync();
            return metricModel;
        }

        public async Task<List<Models.SustainabilityMetrics>> GetAllAsync()
        {
            return await _context.SustainabilityMetrics.ToListAsync();
        }

        public async Task<Models.SustainabilityMetrics?> GetByIdAsync(int id)
        {
            return await _context.SustainabilityMetrics.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.SustainabilityMetrics?> UpdateAsync(int id, UpdateSustainabilityMetricsDto sustainabilityMetricsDto)
        {
            var metricModel = await _context.SustainabilityMetrics.FirstOrDefaultAsync(x => x.Id == id);
            if (metricModel == null)
            {
                return null;
            }
            metricModel.MetricType = sustainabilityMetricsDto.MetricType;
            metricModel.Value = sustainabilityMetricsDto.MetricValue;
            await _context.SaveChangesAsync();
            return metricModel;
        }
    }
}
