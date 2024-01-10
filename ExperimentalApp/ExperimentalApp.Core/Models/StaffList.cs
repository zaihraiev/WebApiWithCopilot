using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a staff member in the staff list.
/// </summary>
public partial class StaffList
{
    /// <summary>
    /// Gets or sets the ID of the staff member.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the staff member.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the address of the staff member.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the zip code of the staff member's address.
    /// </summary>
    public string? ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the staff member.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the city of the staff member's address.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the country of the staff member's address.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the SID of the staff member.
    /// </summary>
    public int? Sid { get; set; }
}
