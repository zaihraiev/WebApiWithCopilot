using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents an inventory item.
/// </summary>
public partial class Inventory
{
    /// <summary>
    /// Gets or sets the inventory ID.
    /// </summary>
    public int InventoryId { get; set; }

    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int FilmId { get; set; }

    /// <summary>
    /// Gets or sets the store ID.
    /// </summary>
    public int StoreId { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the associated film.
    /// </summary>
    public virtual Film Film { get; set; } = null!;

    /// <summary>
    /// Gets or sets the rentals associated with the inventory item.
    /// </summary>
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
