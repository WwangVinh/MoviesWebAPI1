using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? DirectorId { get; set; }

    public decimal? Rating { get; set; }

    public string? PosterUrl { get; set; }

    public string? AvatarUrl { get; set; }

    public string? LinkFilmUrl { get; set; }

    public int Status { get; set; }

    public virtual Director? Director { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
