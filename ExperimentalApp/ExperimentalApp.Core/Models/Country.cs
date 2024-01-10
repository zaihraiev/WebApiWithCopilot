using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a country.
/// </summary>
public partial class Country
{
    /// <summary>
    /// Gets or sets the country ID.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string Country1 { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the cities associated with the country.
    /// </summary>
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
