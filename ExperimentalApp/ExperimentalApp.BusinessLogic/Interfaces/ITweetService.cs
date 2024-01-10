using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.ViewModels;
namespace ExperimentalApp.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface for manipulation with tweets.
    /// </summary>
    public interface ITweetService
    {
        /// <summary>
        /// Adds a new tweet.
        /// </summary>
        /// <param name="tweetCreateViewModel">View model from client for creation new tweet</param>
        /// <returns>Boolean indicator of the success of the operation</returns>
        public Task<bool> AddTweetAsync(TweetCreateViewModel tweetCreateViewModel);

        /// <summary>
        /// Represents a method for getting all tweets.
        /// </summary>
        /// <returns>Returns all tweets</returns>
        public Task<IEnumerable<Tweet>> GetTweetsAsync();

        /// <summary>
        /// Gets a tweet by id.
        /// </summary>
        /// <param name="id">tweet id for searching</param>
        /// <returns>Found tweet</returns>
        public Task<Tweet> GetTweetByIdAsync(string id);

        /// <summary>
        /// Gets filtered tweets.
        /// </summary>
        /// <param name="tweetFilterViewModel">filter params</param>
        /// <returns>Filtered tweets by specific fields</returns>
        public Task<IEnumerable<Tweet>> GetFilteredTweetsAsync(TweetFilterViewModel tweetFilterViewModel);

        /// <summary>
        /// Updates a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to update</param>
        /// <param name="tweetUpdateViewModel">Tweet params to update</param>
        /// <returns>The state of update result</returns>
        public Task<bool> UpdateTweetById(string id, TweetUpdateViewModel tweetUpdateViewModel);

        /// <summary>
        /// Deletes a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to delete</param>
        /// <returns>Boolean indicator of the success of the operation</returns>
        public Task<bool> DeleteTweetByIdAsync(string id);
    }
}
