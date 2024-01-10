using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a payment made by a customer.
/// </summary>
public partial class Payment
{
    /// <summary>
    /// Gets or sets the payment ID.
    /// </summary>
    public int PaymentId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the staff ID.
    /// </summary>
    public int StaffId { get; set; }

    /// <summary>
    /// Gets or sets the rental ID.
    /// </summary>
    public int RentalId { get; set; }

    /// <summary>
    /// Gets or sets the payment amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the associated customer.
    /// </summary>
    public virtual Customer Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated rental.
    /// </summary>
    public virtual Rental Rental { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated staff.
    /// </summary>
    public virtual Staff Staff { get; set; } = null!;
}
