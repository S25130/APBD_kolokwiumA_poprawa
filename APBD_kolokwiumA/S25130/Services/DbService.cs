using S25130.Data;
using S25130.Models;
using Microsoft.EntityFrameworkCore;

namespace S25130.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Nursery?> GetNurseryWithBatches(int nurseryId)
    {
        return await _context.Nurseries
            .Include(n => n.Batches)
            .ThenInclude(b => b.Species)
            .Include(n => n.Batches)
            .ThenInclude(b => b.Responsibles)
            .ThenInclude(r => r.Employee)
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NurseryId == nurseryId);
    }

    public async Task<bool> DoesSpeciesExist(string latinName)
    {
        return await _context.Species.AnyAsync(s => s.LatinName == latinName);
    }

    public async Task<TreeSpecies?> GetSpeciesByName(string latinName)
    {
        return await _context.Species.FirstOrDefaultAsync(s => s.LatinName == latinName);
    }

    public async Task<Nursery?> GetNurseryByName(string name)
    {
        return await _context.Nurseries.FirstOrDefaultAsync(n => n.Name == name);
    }

    public async Task<List<int>> GetExistingEmployeeIds(List<int> ids)
    {
        return await _context.Employees
            .Where(e => ids.Contains(e.EmployeeId))
            .Select(e => e.EmployeeId)
            .ToListAsync();
    }

    public async Task AddSeedlingBatch(SeedlingBatch batch)
    {
        await _context.Batches.AddAsync(batch);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetEmployeesByIds(List<int> ids)
    {
        return await _context.Employees
            .Where(e => ids.Contains(e.EmployeeId))
            .ToListAsync();
    }
}
