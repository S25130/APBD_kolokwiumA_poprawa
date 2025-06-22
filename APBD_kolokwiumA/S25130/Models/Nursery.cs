using System.ComponentModel.DataAnnotations;

namespace S25130.Models;

public class Nursery
{
    [Key]
    public int NurseryId { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }

    public ICollection<SeedlingBatch> Batches { get; set; }
}
