using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a city.
/// </summary>
public partial class City
{
    /// <summary>
    /// Gets or sets the city ID.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public string City1 { get; set; } = null!;

    /// <summary>
    /// Gets or sets the country ID.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the addresses associated with the city.
    /// </summary>
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    /// <summary>
    /// Gets or sets the country associated with the city.
    /// </summary>
    public virtual Country Country { get; set; } = null!;
}
