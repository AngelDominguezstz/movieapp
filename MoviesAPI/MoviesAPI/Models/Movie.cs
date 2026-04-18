using System;
using System.Collections.Generic;

namespace MoviesAPI.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? ReleaseYear { get; set; }

    public string? Gender { get; set; }

    public TimeOnly? Duration { get; set; }

    public int? Fkdirector { get; set; }

    public virtual Director? FkdirectorNavigation { get; set; }
}
