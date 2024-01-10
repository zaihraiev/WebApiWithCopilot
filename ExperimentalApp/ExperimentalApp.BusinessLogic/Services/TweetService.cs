using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.Core.ViewModels;
using ExperimentalApp.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ExperimentalApp.BusinessLogic.Services
{
    /// <summary>
    /// Service for manipulation with tweets.
    /// </summary>
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructs a new instance of the <see cref="TweetService"/> class with neccessary dependencies.
        /// </summary>
        /// <param name="tweetRepository">Repository for work with data base context</param>
        /// <param name="userManager">Service for identity user</param>
        /// <param name="httpContextAccessor">Encapsulates all information about an individual HTTP request and response.</param>
        public TweetService(ITweetRepository tweetRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _tweetRepository = tweetRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Adds a new tweet.
        /// </summary>
        /// <param name="tweetCreateViewModel">View model from client for creation new tweet</param>
        /// <returns>Boolean indicator of the success of the operation</returns>
        public async Task<bool> AddTweetAsync(TweetCreateViewModel tweetCreateViewModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                var tweet = new Tweet
                {
                    Text = tweetCreateViewModel.Text,
                    Timestamp = DateTime.Now.ToUniversalTime(),
                    Tags = tweetCreateViewModel.Tags,
                    UserTweets = new List<UserTweets>
                    {
                        new UserTweets
                        {
                            UserId = userId,
                            User = user
                        }
                    }
                };

                _tweetRepository.AddTweet(tweet);               
            }

            return await _tweetRepository.Complete();
        }

        /// <summary>
        /// Represents a method for getting all tweets.
        /// </summary>
        /// <returns>Returns all tweets</returns>
        public async Task<IEnumerable<Tweet>> GetTweetsAsync()
        {
            return await _tweetRepository.GetTweetsAsync();
        }

        /// <summary>
        /// Gets a tweet by id.
        /// </summary>
        /// <param name="id">tweet id for searching</param>
        /// <returns>Found tweet</returns>
        public async Task<Tweet> GetTweetByIdAsync(string id)
        {
            return await _tweetRepository.GetTweetByIdAsync(id);
        }

        /// <summary>
        /// Returns filtered tweets by specific fields.
        /// </summary>
        /// <param name="tweetFilterViewModel">Params to filter tweets</param>
        /// <returns>Filtered tweets</returns>
        public async Task<IEnumerable<Tweet>> GetFilteredTweetsAsync(TweetFilterViewModel tweetFilterViewModel)
        {
            return await _tweetRepository.GetFilteredTweetsAsync(tweetFilterViewModel);
        }

        /// <summary>
        /// Updates a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to update</param>
        /// <param name="tweetUpdateViewModel">Tweet params to update</param>
        /// <returns>The state of update result</returns>
        public async Task<bool> UpdateTweetById(string id, TweetUpdateViewModel tweetUpdateViewModel)
        {
            var tweet = await _tweetRepository.GetTweetByIdAsync(id);

            if (tweet == null)
            {
                return false;
            }

            if(!string.IsNullOrEmpty(tweetUpdateViewModel.Text))
            {
                tweet.Text = tweetUpdateViewModel.Text;
            }

            if(tweetUpdateViewModel.Tags != null && tweetUpdateViewModel.Tags.Any())
            {
                tweet.Tags = new List<string>(tweetUpdateViewModel.Tags);
            }
            
            _tweetRepository.UpdateTweet(tweet);

            return await _tweetRepository.Complete();
        }

        /// <summary>
        /// Deletes a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to delete</param>
        /// <returns>Boolean indicator of the success of the operation</returns>
        public async Task<bool> DeleteTweetByIdAsync(string id)
        {
            var tweet = await _tweetRepository.GetTweetByIdAsync(id);

            if (tweet == null)
            {
                return false;
            }

            _tweetRepository.DeleteTweet(tweet);

            return await _tweetRepository.Complete();
        }
    }
}
