using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Core.Constants;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExperimentalApp.Controllers
{
    /// <summary>
    /// Represents a controller for tweets.
    /// </summary>
    [Authorize]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        /// <summary>
        /// Constructs a new instance of the <see cref="TweetsController"/> class. 
        /// </summary>
        /// <param name="tweetService">Tweet interface for manipulation with tweets</param>
        public TweetsController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        /// <summary>
        /// Creates a new tweet.
        /// </summary>
        /// <param name="tweetViewModel">The tweet view model.</param>
        /// <returns>The result of the tweet creation.</returns>
        [HttpPost]
        [Route("tweets")]
        public async Task<IActionResult> CreateTweet([FromBody] TweetCreateViewModel tweetViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _tweetService.AddTweetAsync(tweetViewModel);
                
                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Represents a method for getting all tweets.
        /// </summary>
        /// <returns>Returns all tweets</returns>
        [HttpGet]
        [Route("tweets")]
        public async Task<IActionResult> GetTweets()
        {
            var tweets = await _tweetService.GetTweetsAsync();

            if (!tweets.Any())
            {
                return NotFound();
            }

            return Ok(tweets);
        }

        /// <summary>
        /// Retrieves a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to search</param>
        /// <returns>Found tweet</returns>
        [HttpGet]
        [Route("tweets/{id}")]
        public async Task<IActionResult> GetTweetByIdAsync(string id)
        {
            var tweet =  await _tweetService.GetTweetByIdAsync(id);

            if (tweet == null)
            {
                return NotFound();
            }

            return Ok(tweet);
        }

        /// <summary>
        /// Represents a method for getting filtered tweets.
        /// </summary>
        /// <param name="tweetFilterViewModel">Model with params for filtering</param>
        /// <returns>Filtered tweets</returns>
        [HttpGet]
        [Route("tweets/filtered")]
        public async Task<IActionResult> GetFilteredTweets([FromQuery] TweetFilterViewModel tweetFilterViewModel)
        {
            var result = await _tweetService.GetFilteredTweetsAsync(tweetFilterViewModel);

            return Ok(result);
        }

        /// <summary>
        /// Update tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to update</param>
        /// <param name="tweetViewModel">Update params for tweet</param>
        /// <returns>Completed state of execution</returns>
        [HttpPut]
        [Route("tweets/{id}")]
        [Authorize(Roles = RolesConstants.Customer)]
        public async Task<IActionResult> UpdateTweetById(string id, [FromBody] TweetUpdateViewModel tweetViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _tweetService.UpdateTweetById(id, tweetViewModel);

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a tweet by id.
        /// </summary>
        /// <param name="id">Tweet id to delete</param>
        /// <returns>Boolean indicator of the success of the operation</returns>
        [HttpDelete]
        [Route("tweets/{id}")]
        public async Task<IActionResult> DeleteTweetByIdAsync(string id)
        {
            var result = await _tweetService.DeleteTweetByIdAsync(id);

            if(result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
