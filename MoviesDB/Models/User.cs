using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MoviesDB.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public int Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
