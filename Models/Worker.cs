namespace WMSB.Models;

public partial class Worker
{
    public int Id { get; set; }

    public int AssociateId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int PositionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int IsDeleted { get; set; }
}
