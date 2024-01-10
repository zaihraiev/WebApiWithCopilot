using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a rental in the ExperimentalApp.
/// </summary>
public partial class Rental
{
    /// <summary>
    /// Gets or sets the rental ID.
    /// </summary>
    public int RentalId { get; set; }

    /// <summary>
    /// Gets or sets the rental date.
    /// </summary>
    public DateTime RentalDate { get; set; }

    /// <summary>
    /// Gets or sets the inventory ID.
    /// </summary>
    public int InventoryId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the return date.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// Gets or sets the staff ID.
    /// </summary>
    public int StaffId { get; set; }

    /// <summary>
    /// Gets or sets the last update date.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the associated customer.
    /// </summary>
    public virtual Customer Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated inventory.
    /// </summary>
    public virtual Inventory Inventory { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of payments associated with the rental.
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    /// <summary>
    /// Gets or sets the associated staff.
    /// </summary>
    public virtual Staff Staff { get; set; } = null!;
}
