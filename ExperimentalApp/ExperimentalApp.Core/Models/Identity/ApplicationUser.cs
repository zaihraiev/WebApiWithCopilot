using Microsoft.AspNetCore.Identity;

namespace ExperimentalApp.Core.Models.Identity
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class ApplicationUser : IdentityUser 
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of user tweets associated with the user.
        /// </summary>
        public ICollection<UserTweets>? UserTweets { get; set; } = new List<UserTweets>();

        /// <summary>
        /// Gets or sets the collection of user followers associated with the user.
        /// </summary>
        public ICollection<UserFollowers>? Followers { get; set; } = new List<UserFollowers>();

        /// <summary>
        /// Gets or sets the collection of user following associated with the user.
        /// </summary>
        public ICollection<UserFollowers>? Following { get; set; } = new List<UserFollowers>();
    }
}
