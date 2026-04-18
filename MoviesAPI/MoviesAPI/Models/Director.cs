using System;
using System.Collections.Generic;

namespace MoviesAPI.Models;

public partial class Director
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Nationality { get; set; }

    public int? Age { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
