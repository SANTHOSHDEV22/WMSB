namespace WMSB.Models;

public partial class Position
{
    public int Id { get; set; }

    public string Position1 { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
