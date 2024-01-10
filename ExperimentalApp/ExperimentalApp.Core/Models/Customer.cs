using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a customer in the system.
/// </summary>
public partial class Customer
{
    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the store ID.
    /// </summary>
    public int StoreId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the address ID.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Gets or sets the boolean value indicating if the customer is active.
    /// </summary>
    public bool? Activebool { get; set; }

    /// <summary>
    /// Gets or sets the create date of the customer.
    /// </summary>
    public DateOnly CreateDate { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the customer.
    /// </summary>
    public DateTime? LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the active status of the customer.
    /// </summary>
    public int? Active { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public virtual Address Address { get; set; } = null!;

    /// <summary>
    /// Gets or sets the payments made by the customer.
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    /// <summary>
    /// Gets or sets the rentals made by the customer.
    /// </summary>
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
