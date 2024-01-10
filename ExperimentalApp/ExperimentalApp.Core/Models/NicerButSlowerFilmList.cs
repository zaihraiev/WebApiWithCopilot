using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a film in the NicerButSlowerFilmList.
/// </summary>
public partial class NicerButSlowerFilmList
{
    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int? Fid { get; set; }

    /// <summary>
    /// Gets or sets the film title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the film description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the film category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the film price.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the film length.
    /// </summary>
    public short? Length { get; set; }

    /// <summary>
    /// Gets or sets the film actors.
    /// </summary>
    public string? Actors { get; set; }
}
