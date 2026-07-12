namespace WMSB.Models;

public class WorkerListDto
{
    public int Id { get; set; }
    public int AssociateId { get; set; }
    public string FullName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int? PositionId { get; set; }
    public string? PositionName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
