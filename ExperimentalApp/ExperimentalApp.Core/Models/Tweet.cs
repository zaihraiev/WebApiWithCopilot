using System.ComponentModel.DataAnnotations.Schema;

namespace ExperimentalApp.Core.Models
{
    /// <summary>
    /// Represents a tweet.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// Gets or sets the tweet ID.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TweetId { get; set; }

        /// <summary>
        /// Gets or sets the text of the tweet.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the tweet.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the number of likes the tweet has.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with the tweet.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the user tweets associated with the tweet.
        /// </summary>
        public ICollection<UserTweets> UserTweets { get; set; } = new List<UserTweets>();
    }
}
