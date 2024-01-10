using ExperimentalApp.Core.Models.Identity;

namespace ExperimentalApp.Core.Models
{
    /// <summary>
    /// Represents a user follower.
    /// </summary>
    public class UserFollowers
    {
        /// <summary>
        /// Represents the ID of the follower.
        /// </summary>
        public string FollowerId { get; set; }

        /// <summary>
        /// Represents the follower.
        /// </summary>
        public ApplicationUser Follower { get; set; }

        /// <summary>
        /// Represents the ID of the user being followed.
        /// </summary>
        public string FollowingId { get; set; }

        /// <summary>
        /// Represents the user being followed.
        /// </summary>
        public ApplicationUser Following { get; set; }

        /// <summary>
        /// Represents the date the user was followed.
        /// </summary>
        public DateTime FollowedDate { get; set; } = DateTime.UtcNow;
    }
}
