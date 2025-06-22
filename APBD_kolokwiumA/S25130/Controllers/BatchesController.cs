using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S25130.Data;
using S25130.Models;
using S25130.DTOs;


namespace S25130.Controllers;

[ApiController]
[Route("api/batches")]
public class BatchesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BatchesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBatch(CreateBatchDto dto)
    {
        var species = await _context.Species.FirstOrDefaultAsync(s => s.LatinName == dto.Species);
        if (species == null) return BadRequest("Species not found");

        var nursery = await _context.Nurseries.FirstOrDefaultAsync(n => n.Name == dto.Nursery);
        if (nursery == null) return BadRequest("Nursery not found");

        var employeeIds = dto.Responsible.Select(r => r.EmployeeId).ToList();
        var foundEmployees = await _context.Employees
            .Where(e => employeeIds.Contains(e.EmployeeId))
            .Select(e => e.EmployeeId)
            .ToListAsync();

        var missing = employeeIds.Except(foundEmployees).ToList();
        if (missing.Any()) return BadRequest($"Employees not found: {string.Join(", ", missing)}");

        var batch = new SeedlingBatch
        {
            Quantity = dto.Quantity,
            SownDate = DateTime.UtcNow,
            ReadyDate = DateTime.UtcNow.AddYears(species.GrowthTimeInYears),
            NurseryId = nursery.NurseryId,
            SpeciesId = species.SpeciesId,
            Responsibles = dto.Responsible.Select(r => new Responsible
            {
                EmployeeId = r.EmployeeId,
                Role = r.Role
            }).ToList()
        };

        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateBatch), new { id = batch.BatchId }, null);
    }
}
