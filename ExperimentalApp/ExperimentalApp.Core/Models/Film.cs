using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a film.
/// </summary>
public partial class Film
{
    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int FilmId { get; set; }

    /// <summary>
    /// Gets or sets the title of the film.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the film.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the release year of the film.
    /// </summary>
    public int? ReleaseYear { get; set; }

    /// <summary>
    /// Gets or sets the language ID of the film.
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// Gets or sets the rental duration of the film.
    /// </summary>
    public short RentalDuration { get; set; }

    /// <summary>
    /// Gets or sets the rental rate of the film.
    /// </summary>
    public decimal RentalRate { get; set; }

    /// <summary>
    /// Gets or sets the length of the film.
    /// </summary>
    public short? Length { get; set; }

    /// <summary>
    /// Gets or sets the replacement cost of the film.
    /// </summary>
    public decimal ReplacementCost { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the film.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the special features of the film.
    /// </summary>
    public string[]? SpecialFeatures { get; set; }

    /// <summary>
    /// Gets or sets the fulltext search vector of the film.
    /// </summary>
    public NpgsqlTsVector Fulltext { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of film actors.
    /// </summary>
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    /// <summary>
    /// Gets or sets the collection of film categories.
    /// </summary>
    public virtual ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();

    /// <summary>
    /// Gets or sets the collection of inventories.
    /// </summary>
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    /// <summary>
    /// Gets or sets the language of the film.
    /// </summary>
    public virtual Language Language { get; set; } = null!;
}
