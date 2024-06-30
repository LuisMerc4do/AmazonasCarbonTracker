using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazonasCarbonTracker.Dtos;
using AmazonasCarbonTracker.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazonasCarbonTracker.Dtos.EmissionRecordDtos;
using AmazonasCarbonTracker.Data;
using Microsoft.AspNetCore.Authorization;

namespace AmazonasCarbonTracker.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmissionRecordsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EmissionRecordsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmissionRecordDto>>> GetEmissionRecords()
        {
            try
            {
                var emissionRecords = await _context.EmissionRecords.ToListAsync();
                var emissionRecordDtos = emissionRecords.Select(e => new EmissionRecordDto
                {
                    Id = e.Id,
                    Date = e.Date,
                    ActivityType = e.ActivityType,
                    EmissionAmount = e.EmissionAmount,
                    AppUserId = User.IsInRole("Admin") ? e.AppUserId : null
                }).ToList();
                return Ok(emissionRecordDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving emission records");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmissionRecordDto>> GetEmissionRecord(int id)
        {
            try
            {
                var emissionRecord = await _context.EmissionRecords.FindAsync(id);

                if (emissionRecord == null)
                {
                    return NotFound();
                }

                var emissionRecordDto = new EmissionRecordDto
                {
                    Id = emissionRecord.Id,
                    Date = emissionRecord.Date,
                    ActivityType = emissionRecord.ActivityType,
                    EmissionAmount = emissionRecord.EmissionAmount,
                    AppUserId = User.IsInRole("Admin") ? emissionRecord.AppUserId : null
                };

                return Ok(emissionRecordDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error retrieving emission record with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmissionRecordDto>> PostEmissionRecord(CreateEmissionDto createDto)
        {
            try
            {
                // Check if the AppUserId exists in AspNetUsers
                var user = await _context.Users.FindAsync(createDto.AppUserId);

                if (user == null)
                {
                    Log.Warning($"Invalid AppUserId. User does not exist: {createDto.AppUserId}");
                    return BadRequest("Invalid AppUserId. User does not exist.");
                }

                var emissionRecord = new EmissionRecord
                {
                    Date = DateTime.Now,
                    ActivityType = createDto.ActivityType,
                    EmissionAmount = createDto.EmissionAmount,
                    AppUserId = User.IsInRole("Admin") ? createDto.AppUserId : null
                };

                _context.EmissionRecords.Add(emissionRecord);
                await _context.SaveChangesAsync();

                Log.Information($"Emission record created successfully. Id: {emissionRecord.Id}");
                return CreatedAtAction(nameof(GetEmissionRecord), new { id = emissionRecord.Id }, emissionRecord);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating emission record");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmissionRecord(int id, UpdateEmissionDto updateDto)
        {
            try
            {
                var emissionRecord = await _context.EmissionRecords.FindAsync(id);

                if (emissionRecord == null)
                {
                    return NotFound();
                }

                emissionRecord.Date = updateDto.Date;
                emissionRecord.ActivityType = updateDto.ActivityType;
                emissionRecord.EmissionAmount = updateDto.EmissionAmount;

                await _context.SaveChangesAsync();

                Log.Information($"Emission record updated successfully. Id: {id}");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmissionRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error updating emission record with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmissionRecord(int id)
        {
            try
            {
                var emissionRecord = await _context.EmissionRecords.FindAsync(id);

                if (emissionRecord == null)
                {
                    return NotFound();
                }

                _context.EmissionRecords.Remove(emissionRecord);
                await _context.SaveChangesAsync();

                Log.Information($"Emission record deleted successfully. Id: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error deleting emission record with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool EmissionRecordExists(int id)
        {
            return _context.EmissionRecords.Any(e => e.Id == id);
        }
    }
}
