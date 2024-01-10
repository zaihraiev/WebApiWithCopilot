using ExperimentalApp.Core.Models.Identity;

namespace ExperimentalApp.Core.Models
{   
    ///<summary>
    /// Represents a user's tweets.
    /// </summary>
    public class UserTweets
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the tweets.
        /// </summary>
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// Gets or sets the tweet ID.
        /// </summary>
        public string? TweetId { get; set; }

        /// <summary>
        /// Gets or sets the tweet associated with the user.
        /// </summary>
        public Tweet? Tweet { get; set; }
    }
}
