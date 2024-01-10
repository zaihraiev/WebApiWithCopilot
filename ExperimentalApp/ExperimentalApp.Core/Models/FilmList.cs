using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a film in the film list.
/// </summary>
public partial class FilmList
{
    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int? Fid { get; set; }

    /// <summary>
    /// Gets or sets the title of the film.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the film.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the film.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the price of the film.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the length of the film.
    /// </summary>
    public short? Length { get; set; }

    /// <summary>
    /// Gets or sets the actors in the film.
    /// </summary>
    public string? Actors { get; set; }
}
