namespace WMSB.Models;

public partial class PunchRecord
{
    public int Id { get; set; }

    public string PunchType { get; set; } = null!;

    public DateOnly PunchDate { get; set; }

    public TimeSpan PunchTime { get; set; }

    public DateTime CreatedAt { get; set; }
}
