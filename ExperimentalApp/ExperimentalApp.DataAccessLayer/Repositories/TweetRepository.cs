using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.ViewModels;
using ExperimentalApp.DataAccessLayer.DataDbContext;
using ExperimentalApp.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExperimentalApp.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents the tweet repository class for working with the database context.
    /// </summary>
    public class TweetRepository : ITweetRepository
    {
        private readonly DvdRentalContext _context;

        /// <summary>
        /// Constructor for the tweet repository.
        /// </summary>
        /// <param name="context">Represent data base context</param>
        public TweetRepository(DvdRentalContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Represents a method for adding a new tweet.
        /// </summary>
        /// <param name="tweet">Model that should be created</param>
        public void AddTweet(Tweet tweet)
        {
            _context.Tweets.Add(tweet);
        }

        /// <summary>
        /// Represents a method for getting all tweets.
        /// </summary>
        /// <returns>Return all tweets</returns>
        public async Task<IEnumerable<Tweet>> GetTweetsAsync()
        {
            return await _context.Tweets
                .Include(ut => ut.UserTweets)
                .ThenInclude(u => u.User).ToListAsync();
        }

        /// <summary>
        /// Represents a method for saving changes to the database.
        /// </summary>
        /// <returns>Returns true if the models have been changed</returns>
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Gets a tweet by id.
        /// </summary>
        /// <param name="id">tweet id for searching</param>
        /// <returns>Found tweet</returns>
        public async Task<Tweet> GetTweetByIdAsync(string id)
        {
            return await _context.Tweets
                .Include(ut => ut.UserTweets)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.TweetId == id);
        }

        /// <summary>
        /// Gets filtered tweets.
        /// </summary>
        /// <param name="tweetFilterViewModel">params to filter tweets</param>
        /// <returns>filtered tweets</returns>
        public async Task<IEnumerable<Tweet>> GetFilteredTweetsAsync(TweetFilterViewModel tweetFilterViewModel)
        {
            return await _context.Tweets
                .Include(ut => ut.UserTweets)
                .ThenInclude(t => t.User)
                .Where(t => (tweetFilterViewModel.Text == null || t.Text!.Contains(tweetFilterViewModel.Text)) &&
                            (tweetFilterViewModel.Timestamp == default(DateTime) || t.Timestamp == tweetFilterViewModel.Timestamp) &&
                            (tweetFilterViewModel.Tags == null || t.Tags!.Any(tag => tweetFilterViewModel.Tags.Contains(tag))) &&
                            (tweetFilterViewModel.UserName == null || t.UserTweets.Any(ut => ut.User!.UserName == tweetFilterViewModel.UserName)))
                .ToListAsync();
        }

        /// <summary>
        /// Updates a tweet.
        /// </summary>
        public void UpdateTweet(Tweet tweet)
        {
            _context.Tweets.Update(tweet);
        }

        /// <inheritdoc />
        public void DeleteTweet(Tweet tweet)
        {
            _context.Tweets.Remove(tweet);
        }
    }
}
