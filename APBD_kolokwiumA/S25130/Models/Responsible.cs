using System.ComponentModel.DataAnnotations;

namespace S25130.Models;

public class Responsible
{
    [Key]
    public int BatchId { get; set; }
    public int EmployeeId { get; set; }
    [MaxLength(100)]
    public string Role { get; set; }

    public SeedlingBatch Batch { get; set; }
    public Employee Employee { get; set; }
}
