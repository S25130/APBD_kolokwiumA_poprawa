using System.ComponentModel.DataAnnotations;

namespace S25130.Models;

public class TreeSpecies
{
    [Key]
    public int SpeciesId { get; set; }
    public string LatinName { get; set; }
    public int GrowthTimeInYears { get; set; }

    public ICollection<SeedlingBatch> Batches { get; set; }
}
