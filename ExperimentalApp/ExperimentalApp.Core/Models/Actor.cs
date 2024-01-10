using System;
using System.Collections.Generic;

namespace ExperimentalApp.Core.Models;
/// <summary>
/// Represents an actor in the experimental application.
/// </summary>
public partial class Actor
{
    /// <summary>
    /// Gets or sets the actor ID.
    /// </summary>
    public int ActorId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the actor.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the actor.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last update date and time of the actor.
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Gets or sets the collection of film actors associated with the actor.
    /// </summary>
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
