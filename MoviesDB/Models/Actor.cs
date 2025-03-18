using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Actor
{
    public int ActorsId { get; set; }

    public string NameAct { get; set; } = null!;

    public string? Description { get; set; }

    public string? Nationlity { get; set; }

    public string? Professional { get; set; }

    public string? AvatarUrl { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
