using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents the sales by film category.
/// </summary>
public partial class SalesByFilmCategory
{
    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the total sales.
    /// </summary>
    public decimal? TotalSales { get; set; }
}
