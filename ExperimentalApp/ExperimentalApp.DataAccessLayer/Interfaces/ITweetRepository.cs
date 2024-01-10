using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.ViewModels;

namespace ExperimentalApp.DataAccessLayer.Interfaces
{
    /// <summary>
    /// Represents the tweet repository interface for working with the database context.
    /// </summary>
    public interface ITweetRepository
    {
        /// <summary>
        ///  Represents a method for adding a new tweet.
        /// </summary>
        /// <param name="tweet">Model that should be created</param>
        public void AddTweet(Tweet tweet);

        /// <summary>
        /// Represents a method for getting all tweets.
        /// </summary>
        /// <returns>Return all tweets from database</returns>
        public Task<IEnumerable<Tweet>> GetTweetsAsync();

        /// <summary>
        /// Represents a method for saving changes to the database.
        /// </summary>
        /// <returns>Returns true if the models have been changed</returns>
        public Task<bool> Complete();

        /// <summary>
        /// Gets a tweet by id.
        /// </summary>
        /// <param name="id">tweet id for searching</param>
        /// <returns>Found tweet</returns>
        public Task<Tweet> GetTweetByIdAsync(string id);

        /// <summary>
        /// Gets filtered tweets.
        /// </summary>
        /// <param name="tweetFilterViewModel">params to filter tweets</param>
        /// <returns>filtered tweets</returns>
        public Task<IEnumerable<Tweet>> GetFilteredTweetsAsync(TweetFilterViewModel tweetFilterViewModel);

        /// <summary>
        /// Updates a tweet.
        /// </summary>
        public void UpdateTweet(Tweet tweet);

        /// <summary>
        /// Deletes a tweet
        /// </summary>
        /// <param name="tweet">Tweet to delete</param>
        public void DeleteTweet(Tweet tweet);
    }
}
