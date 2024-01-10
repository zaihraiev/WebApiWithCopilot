using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents an actor's information.
/// </summary>
public partial class ActorInfo
{
    /// <summary>
    /// Gets or sets the actor's ID.
    /// </summary>
    public int? ActorId { get; set; }

    /// <summary>
    /// Gets or sets the actor's first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the actor's last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the actor's film information.
    /// </summary>
    public string? FilmInfo { get; set; }
}
