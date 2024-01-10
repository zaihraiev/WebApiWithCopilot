using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents the sales data for a specific store.
/// </summary>
public partial class SalesByStore
{
    /// <summary>
    /// Gets or sets the name of the store.
    /// </summary>
    public string? Store { get; set; }

    /// <summary>
    /// Gets or sets the name of the store manager.
    /// </summary>
    public string? Manager { get; set; }

    /// <summary>
    /// Gets or sets the total sales amount for the store.
    /// </summary>
    public decimal? TotalSales { get; set; }
}
