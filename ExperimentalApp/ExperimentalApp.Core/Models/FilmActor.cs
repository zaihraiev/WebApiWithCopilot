using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents a film actor.
/// </summary>
public partial class FilmActor
{
    /// <summary>
    /// Gets or sets the actor ID.
    /// </summary>
    public int ActorId { get; set; }

    /// <summary>
    /// Gets or sets the film ID.
    /// </summary>
    public int FilmId { get; set; }

    /// <summary>
    /// Gets or sets the last update date and time.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the associated actor.
    /// </summary>
    public virtual Actor Actor { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated film.
    /// </summary>
    public virtual Film Film { get; set; } = null!;
}
