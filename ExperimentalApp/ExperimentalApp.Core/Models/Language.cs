using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a language in the application.
/// </summary>
public partial class Language
{
    /// <summary>
    /// Gets or sets the language ID.
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last update date and time of the language.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the films associated with the language.
    /// </summary>
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
