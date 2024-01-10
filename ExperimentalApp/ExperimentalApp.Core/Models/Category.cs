using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a category in the ExperimentalApp.
/// </summary>
public partial class Category
{
    /// <summary>
    /// Gets or sets the category ID.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last update date and time of the category.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the film categories associated with the category.
    /// </summary>
    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();
}
