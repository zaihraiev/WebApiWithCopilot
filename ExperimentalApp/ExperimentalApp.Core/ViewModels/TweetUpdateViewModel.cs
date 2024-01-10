namespace ExperimentalApp.Core.ViewModels
{
    /// <summary>
    /// Represents a tweet update view model.
    /// </summary>
    public class TweetUpdateViewModel
    {
        /// <summary>
        /// Represents a tweet text to update.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Represents a tweet tags to update.
        /// </summary>
        public List<string>? Tags { get; set; }
    }
}
