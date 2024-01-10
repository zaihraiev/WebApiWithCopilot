using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a staff member.
/// </summary>
public partial class Staff
{
    /// <summary>
    /// Gets or sets the staff ID.
    /// </summary>
    public int StaffId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the staff member.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the staff member.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the address ID of the staff member.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Gets or sets the email of the staff member.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the store ID of the staff member.
    /// </summary>
    public int StoreId { get; set; }

    /// <summary>
    /// Gets or sets the active status of the staff member.
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// Gets or sets the username of the staff member.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the staff member.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time of the staff member.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the picture of the staff member.
    /// </summary>
    public byte[]? Picture { get; set; }

    /// <summary>
    /// Gets or sets the address of the staff member.
    /// </summary>
    public virtual Address Address { get; set; } = null!;

    /// <summary>
    /// Gets or sets the payments made by the staff member.
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    /// <summary>
    /// Gets or sets the rentals made by the staff member.
    /// </summary>
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    /// <summary>
    /// Gets or sets the store associated with the staff member.
    /// </summary>
    public virtual Store? Store { get; set; }
}
