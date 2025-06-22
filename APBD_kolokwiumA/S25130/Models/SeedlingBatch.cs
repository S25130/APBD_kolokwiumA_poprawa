using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S25130.Models;

public class SeedlingBatch
{
    [Key]
    public int BatchId { get; set; }
    [ForeignKey(nameof(NurseryId))]
    public int NurseryId { get; set; }
    [ForeignKey(nameof(SpeciesId))]
    public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; } = null;

    public Nursery Nursery { get; set; }
    public TreeSpecies Species { get; set; }
    public ICollection<Responsible> Responsibles { get; set; }
}
