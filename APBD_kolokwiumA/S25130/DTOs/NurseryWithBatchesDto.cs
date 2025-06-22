namespace S25130.DTOs;

public class NurseryWithBatchesDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public List<BatchDto> Batches { get; set; }
}

public class BatchDto
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    public SpeciesDto Species { get; set; }
    public List<ResponsibleDto> Responsible { get; set; }
}

public class SpeciesDto
{
    public string LatinName { get; set; }
    public int GrowthTimeInYears { get; set; }
}

public class ResponsibleDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}
