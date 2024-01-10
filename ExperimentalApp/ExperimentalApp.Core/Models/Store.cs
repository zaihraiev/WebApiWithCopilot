using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a store.
/// </summary>
public partial class Store
{
    /// <summary>
    /// Gets or sets the store ID.
    /// </summary>
    public int StoreId { get; set; }

    /// <summary>
    /// Gets or sets the manager staff ID.
    /// </summary>
    public int ManagerStaffId { get; set; }

    /// <summary>
    /// Gets or sets the address ID.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the address associated with the store.
    /// </summary>
    public virtual Address Address { get; set; } = null!;

    /// <summary>
    /// Gets or sets the manager staff associated with the store.
    /// </summary>
    public virtual Staff ManagerStaff { get; set; } = null!;
}
