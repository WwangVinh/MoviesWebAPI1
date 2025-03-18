using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Payment
{
    public int SubpaymentId { get; set; }

    public int? UserId { get; set; }

    public string? PlanName { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int Status { get; set; }

    public virtual User? User { get; set; }
}
