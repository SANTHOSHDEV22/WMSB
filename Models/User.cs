namespace WMSB.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Contact { get; set; }

    public string? PasswordHash { get; set; }

    public string? GoogleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public sbyte IsDeleted { get; set; }

    public virtual ICollection<PunchRecord> PunchRecords { get; set; } = new List<PunchRecord>();
}