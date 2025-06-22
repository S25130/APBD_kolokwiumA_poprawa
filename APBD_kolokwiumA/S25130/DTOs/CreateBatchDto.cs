namespace S25130.DTOs;

public class CreateBatchDto
{
    public int Quantity { get; set; }
    public string Species { get; set; }
    public string Nursery { get; set; }
    public List<CreateResponsibleDto> Responsible { get; set; }
}

public class CreateResponsibleDto
{
    public int EmployeeId { get; set; }
    public string Role { get; set; }
}
