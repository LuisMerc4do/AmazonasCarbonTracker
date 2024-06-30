using AmazonasCarbonTracker.Data;
using AmazonasCarbonTracker.Dtos.SustainabilityMetricsDto;
using AmazonasCarbonTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace AmazonasCarbonTracker.Repository
{
    public class SustainabilityMetricsRepo : ISustainabilityMetrics
    {
        private readonly ApplicationDBContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<SustainabilityMetricsRepo> _logger;

        public SustainabilityMetricsRepo(ApplicationDBContext context, IMemoryCache cache, ILogger<SustainabilityMetricsRepo> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Models.SustainabilityMetrics> CreateAsync(Models.SustainabilityMetrics sustainabilityMetricsDto)
        {
            await _context.SustainabilityMetrics.AddAsync(sustainabilityMetricsDto);
            await _context.SaveChangesAsync();
            _cache.Remove("sustainabilityMetrics"); // Invalidate cache
            _logger.LogInformation("SustainabilityMetric created and cache invalidated.");
            return sustainabilityMetricsDto;
        }

        public async Task<Models.SustainabilityMetrics?> DeleteAsync(int id)
        {
            var metricModel = await _context.SustainabilityMetrics.FirstOrDefaultAsync(x => x.Id == id);
            if (metricModel == null)
            {
                _logger.LogWarning("SustainabilityMetric with ID {Id} not found.", id);
                return null;
            }
            _context.SustainabilityMetrics.Remove(metricModel);
            await _context.SaveChangesAsync();
            _cache.Remove("sustainabilityMetrics"); // Invalidate cache
            _logger.LogInformation("SustainabilityMetric deleted and cache invalidated.");
            return metricModel;
        }

        public async Task<List<Models.SustainabilityMetrics>> GetAllAsync()
        {
            if (!_cache.TryGetValue("sustainabilityMetrics", out List<Models.SustainabilityMetrics> sustainabilityMetrics))
            {
                sustainabilityMetrics = await _context.SustainabilityMetrics.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set("sustainabilityMetrics", sustainabilityMetrics, cacheEntryOptions);
                _logger.LogInformation("SustainabilityMetrics retrieved from database and cached.");
            }
            else
            {
                _logger.LogInformation("SustainabilityMetrics retrieved from cache.");
            }

            return sustainabilityMetrics;
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
                _logger.LogWarning("SustainabilityMetric with ID {Id} not found.", id);
                return null;
            }
            metricModel.MetricType = sustainabilityMetricsDto.MetricType;
            metricModel.Value = sustainabilityMetricsDto.MetricValue;
            await _context.SaveChangesAsync();
            _cache.Remove("sustainabilityMetrics"); // Invalidate cache
            _logger.LogInformation("SustainabilityMetric updated and cache invalidated.");
            return metricModel;
        }
    }
}
