namespace ExperimentalApp.Core.ViewModels
{
    /// <summary>
    /// Represents the view model for tweet filtering.
    /// </summary>
    public class TweetFilterViewModel
    {
        /// <summary>
        /// sets the text of the tweet.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// sets the timestamp of the tweet.
        /// </summary>
        public DateTime Timestamp { get; set; }

        ///                      
        /// <summary>
        /// sets the tags associated with the tweet.
        /// </summary>
        public List<string>? Tags { get; set; }

        ///                      
        /// <summary>
        /// sets the user tweets associated with the tweet.
        /// </summary>
        public string? UserName { get; set; }
    }
}
