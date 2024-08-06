using System;
using System.Collections.Generic;

namespace AeMAPI.Models;

public partial class Incidentreport
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? Createdat { get; set; }
}
