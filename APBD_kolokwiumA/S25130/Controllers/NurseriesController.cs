using S25130.Data;
using S25130.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace S25130.Controllers;

[ApiController]
[Route("api/nurseries")]
public class NurseriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public NurseriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetBatches(int id)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.Batches)
            .ThenInclude(b => b.Species)
            .Include(n => n.Batches)
            .ThenInclude(b => b.Responsibles)
            .ThenInclude(r => r.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == id);

        if (nursery == null) return NotFound();

        var result = new NurseryWithBatchesDto
        {
            NurseryId = nursery.NurseryId,
            Name = nursery.Name,
            EstablishedDate = nursery.EstablishedDate,
            Batches = nursery.Batches.Select(b => new BatchDto
            {
                BatchId = b.BatchId,
                Quantity = b.Quantity,
                SownDate = b.SownDate,
                ReadyDate = b.ReadyDate,
                Species = new SpeciesDto
                {
                    LatinName = b.Species.LatinName,
                    GrowthTimeInYears = b.Species.GrowthTimeInYears
                },
                Responsible = b.Responsibles.Select(r => new ResponsibleDto
                {
                    FirstName = r.Employee.FirstName,
                    LastName = r.Employee.LastName,
                    Role = r.Role
                }).ToList()
            }).ToList()
        };

        return Ok(result);
    }
}
