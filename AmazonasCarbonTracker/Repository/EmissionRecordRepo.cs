using AmazonasCarbonTracker.Data;
using AmazonasCarbonTracker.Dtos;
using AmazonasCarbonTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace AmazonasCarbonTracker.Repository
{
    public class EmissionRecordRepo : IEmissionRecordRepo
    {
        private readonly ApplicationDBContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger<EmissionRecordRepo> _logger;

        public EmissionRecordRepo(ApplicationDBContext context, IMemoryCache cache, ILogger<EmissionRecordRepo> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Models.EmissionRecord> CreateAsync(Models.EmissionRecord emissionRecord)
        {
            await _context.EmissionRecords.AddAsync(emissionRecord);
            await _context.SaveChangesAsync();
            _cache.Remove("emissionRecords"); // Invalidate cache
            _logger.LogInformation("EmissionRecord created and cache invalidated.");
            return emissionRecord;
        }

        public async Task<Models.EmissionRecord?> DeleteAsync(int id)
        {
            var emissionModel = await _context.EmissionRecords.FirstOrDefaultAsync(x => x.Id == id);
            if (emissionModel == null)
            {
                _logger.LogWarning("EmissionRecord with ID {Id} not found.", id);
                return null;
            }
            _context.EmissionRecords.Remove(emissionModel);
            await _context.SaveChangesAsync();
            _cache.Remove("emissionRecords"); // Invalidate cache
            _logger.LogInformation("EmissionRecord deleted and cache invalidated.");
            return emissionModel;
        }

        public async Task<List<Models.EmissionRecord>> GetAllAsync()
        {
            if (!_cache.TryGetValue("emissionRecords", out List<Models.EmissionRecord> emissionRecords))
            {
                emissionRecords = await _context.EmissionRecords.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set("emissionRecords", emissionRecords, cacheEntryOptions);
                _logger.LogInformation("EmissionRecords retrieved from database and cached.");
            }
            else
            {
                _logger.LogInformation("EmissionRecords retrieved from cache.");
            }

            return emissionRecords;
        }

        public async Task<Models.EmissionRecord?> GetByIdAsync(int id)
        {
            return await _context.EmissionRecords.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.EmissionRecord?> UpdateAsync(int id, UpdateEmissionDto emissionDto)
        {
            var emissionModel = await _context.EmissionRecords.FirstOrDefaultAsync(x => x.Id == id);
            if (emissionModel == null)
            {
                _logger.LogWarning("EmissionRecord with ID {Id} not found.", id);
                return null;
            }
            emissionModel.EmissionAmount = emissionDto.EmissionAmount;
            emissionModel.ActivityType = emissionDto.ActivityType;
            emissionModel.Date = emissionDto.Date;
            await _context.SaveChangesAsync();
            _cache.Remove("emissionRecords"); // Invalidate cache
            _logger.LogInformation("EmissionRecord updated and cache invalidated.");
            return emissionModel;
        }
    }
}
