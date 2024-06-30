using AmazonasCarbonTracker.Data;
using AmazonasCarbonTracker.Dtos;
using AmazonasCarbonTracker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonasCarbonTracker.Repository
{
    public class EmissionRecordRepo : IEmissionRecordRepo
    {
        private readonly ApplicationDBContext _context;

        public EmissionRecordRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Models.EmissionRecord> CreateAsync(Models.EmissionRecord emissionRecord)
        {
            await _context.EmissionRecords.AddAsync(emissionRecord);
            await _context.SaveChangesAsync();
            return emissionRecord;
        }

        public async Task<Models.EmissionRecord?> DeleteAsync(int id)
        {
            var emissionModel = await _context.EmissionRecords.FirstOrDefaultAsync(x => x.Id == id);
            if (emissionModel == null)
            {
                return null;
            }
            _context.EmissionRecords.Remove(emissionModel);
            await _context.SaveChangesAsync();
            return emissionModel;
        }

        public async Task<List<Models.EmissionRecord>> GetAllAsync()
        {
            return await _context.EmissionRecords.ToListAsync();
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
                return null;
            }
            emissionModel.EmissionAmount = emissionDto.EmissionAmount;
            emissionModel.ActivityType = emissionDto.ActivityType;
            emissionModel.Date = emissionDto.Date;
            await _context.SaveChangesAsync();
            return emissionModel;
        }
    }
}
