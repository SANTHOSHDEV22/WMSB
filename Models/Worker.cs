using System;
using System.Collections.Generic;

namespace WMSB.Models;

public partial class Worker
{
    public int Id { get; set; }

    public int? AssociateId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? PositionId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }

    public virtual Position? Position { get; set; }
}
