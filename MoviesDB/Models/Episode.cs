using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Episode
{
    public int EpisodeId { get; set; }

    public int? SeriesId { get; set; }

    public int EpisodeNumber { get; set; }

    public string? Title { get; set; }

    public string LinkFilmUrl { get; set; } = null!;

    public int Status { get; set; }

    public virtual Series? Series { get; set; }
}
