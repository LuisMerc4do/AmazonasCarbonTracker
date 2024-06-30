using AmazonasCarbonTracker.Data;
using AmazonasCarbonTracker.Dtos.SustainabilityMetricsDto;
using AmazonasCarbonTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonasCarbonTracker.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SustainabilityMetricsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SustainabilityMetricsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/SustainabilityMetrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SustainabilityMetricsDto>>> GetSustainabilityMetrics()
        {
            try
            {
                var sustainabilityMetrics = await _context.SustainabilityMetrics.ToListAsync();
                var sustainabilityMetricsDtos = sustainabilityMetrics.Select(s => new SustainabilityMetricsDto
                {
                    Id = s.Id,
                    MetricType = s.MetricType,
                    MetricValue = s.Value,
                    AppUserId = User.IsInRole("Admin") ? s.AppUserId : null
                }).ToList();
                return Ok(sustainabilityMetricsDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to retrieve sustainability metrics");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/SustainabilityMetrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SustainabilityMetricsDto>> GetSustainabilityMetric(int id)
        {
            try
            {
                var sustainabilityMetric = await _context.SustainabilityMetrics.FindAsync(id);

                if (sustainabilityMetric == null)
                {
                    return NotFound();
                }

                var sustainabilityMetricDto = new SustainabilityMetricsDto
                {
                    Id = sustainabilityMetric.Id,
                    MetricType = sustainabilityMetric.MetricType,
                    MetricValue = sustainabilityMetric.Value,
                };

                return Ok(sustainabilityMetricDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to retrieve sustainability metric with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/SustainabilityMetrics
        [HttpPost]
        public async Task<ActionResult<SustainabilityMetricsDto>> PostSustainabilityMetric(CreateSustainabilityMetricsDto createDto)
        {
            try
            {
                // Check if the AppUserId exists in AspNetUsers
                var user = await _context.Users.FindAsync(createDto.AppUserId);

                if (user == null)
                {
                    Log.Warning("Invalid AppUserId. User does not exist: {AppUserId}", createDto.AppUserId);
                    return BadRequest("Invalid AppUserId. User does not exist.");
                }

                var sustainabilityMetric = new SustainabilityMetrics
                {
                    MetricType = createDto.MetricType,
                    Value = createDto.MetricValue,
                    AppUserId = User.IsInRole("Admin") ? createDto.AppUserId : null
                };

                _context.SustainabilityMetrics.Add(sustainabilityMetric);
                await _context.SaveChangesAsync();

                Log.Information("Sustainability metric created successfully. Id: {Id}", sustainabilityMetric.Id);
                return CreatedAtAction(nameof(GetSustainabilityMetric), new { id = sustainabilityMetric.Id }, sustainabilityMetric);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create sustainability metric");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/SustainabilityMetrics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSustainabilityMetric(int id, UpdateSustainabilityMetricsDto updateDto)
        {
            try
            {
                var sustainabilityMetric = await _context.SustainabilityMetrics.FindAsync(id);

                if (sustainabilityMetric == null)
                {
                    return NotFound();
                }

                sustainabilityMetric.MetricType = updateDto.MetricType;
                sustainabilityMetric.Value = updateDto.MetricValue;

                await _context.SaveChangesAsync();

                Log.Information("Sustainability metric updated successfully. Id: {Id}", id);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SustainabilityMetricExists(id))
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
                Log.Error(ex, $"Failed to update sustainability metric with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/SustainabilityMetrics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSustainabilityMetric(int id)
        {
            try
            {
                var sustainabilityMetric = await _context.SustainabilityMetrics.FindAsync(id);

                if (sustainabilityMetric == null)
                {
                    return NotFound();
                }

                _context.SustainabilityMetrics.Remove(sustainabilityMetric);
                await _context.SaveChangesAsync();

                Log.Information("Sustainability metric deleted successfully. Id: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to delete sustainability metric with id {id}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool SustainabilityMetricExists(int id)
        {
            return _context.SustainabilityMetrics.Any(s => s.Id == id);
        }
    }
}
