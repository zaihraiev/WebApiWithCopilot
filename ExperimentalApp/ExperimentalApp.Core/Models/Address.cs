using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents an address.
/// </summary>
public partial class Address
{
    /// <summary>
    /// Gets or sets the address ID.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Gets or sets the first line of the address.
    /// </summary>
    public string Address1 { get; set; } = null!;

    /// <summary>
    /// Gets or sets the second line of the address.
    /// </summary>
    public string? Address2 { get; set; }

    /// <summary>
    /// Gets or sets the district of the address.
    /// </summary>
    public string District { get; set; } = null!;

    /// <summary>
    /// Gets or sets the city ID.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the phone number associated with the address.
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last update date and time of the address.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the city associated with the address.
    /// </summary>
    public virtual City City { get; set; } = null!;

    /// <summary>
    /// Gets or sets the customers associated with the address.
    /// </summary>
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    /// <summary>
    /// Gets or sets the staff associated with the address.
    /// </summary>
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    /// <summary>
    /// Gets or sets the stores associated with the address.
    /// </summary>
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
