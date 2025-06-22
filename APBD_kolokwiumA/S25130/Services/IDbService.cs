using S25130.Models;

namespace S25130.Services;

public interface IDbService
{
    Task<Nursery?> GetNurseryWithBatches(int nurseryId);
    Task<bool> DoesSpeciesExist(string latinName);
    Task<TreeSpecies?> GetSpeciesByName(string latinName);
    Task<Nursery?> GetNurseryByName(string name);
    Task<List<int>> GetExistingEmployeeIds(List<int> ids);
    Task<List<Employee>> GetEmployeesByIds(List<int> ids);
    Task AddSeedlingBatch(SeedlingBatch batch);
}
