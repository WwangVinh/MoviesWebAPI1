using System;
using System.Collections.Generic;

namespace MoviesDB.Models;

public partial class Category
{
    public int CategoriesId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
