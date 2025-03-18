using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Director
{
    public int DirectorId { get; set; }

    public string NameDir { get; set; } = null!;

    public string? Description { get; set; }

    public string? Nationlity { get; set; }

    public string? Professional { get; set; }

    public string? AvatarUrl { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
