using System.ComponentModel.DataAnnotations;

namespace S25130.Models;

public class SeedlingBatch
{
    [Key]
    public int BatchId { get; set; }
    public int NurseryId { get; set; }
    public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }

    public Nursery Nursery { get; set; }
    public TreeSpecies Species { get; set; }
    public ICollection<Responsible> Responsibles { get; set; }
}
