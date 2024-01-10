using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a film category.
/// </summary>
public partial class FilmCategory
{
    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int FilmId { get; set; }

    /// <summary>
    /// Gets or sets the category ID.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the associated category.
    /// </summary>
    public virtual Category Category { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated film.
    /// </summary>
    public virtual Film Film { get; set; } = null!;
}
