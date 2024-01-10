using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a customer in the customer list.
/// </summary>
public partial class CustomerList
{
    /// <summary>
    /// Gets or sets the ID of the customer.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the customer.
    /// </summary>
    public string? ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the city of the customer.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the country of the customer.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets any additional notes about the customer.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the SID of the customer.
    /// </summary>
    public int? Sid { get; set; }
}
