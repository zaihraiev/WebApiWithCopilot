using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ExperimentalApp.BusinessLogic.Services
{
    /// <summary>
    /// Service for followers.
    /// </summary>
    public class FollowersService : IFollowersService
    {
        private readonly IFollowersRepository _followersRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor for followers service.
        /// </summary>
        /// <param name="followersRepository">Repository for working with users</param>
        /// <param name="userManager">Service for identity user</param>
        public FollowersService(IFollowersRepository followersRepository, UserManager<ApplicationUser> userManager)
        {
            _followersRepository = followersRepository;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<bool> AddFollowerAsync(string userId, string followerId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var follower = await _userManager.FindByIdAsync(followerId);

            var relationExist = await _followersRepository.DoesUserHaveFollowerAsync(userId, followerId);

            if (user != null && follower != null && !relationExist)
            {
                var userFollower = new UserFollowers
                {
                    FollowerId = followerId,
                    Follower = follower,
                    FollowingId = userId,
                    Following = user
                };

                _followersRepository.AddFollower(userFollower);
            }

            return await _followersRepository.Complete();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteFollowerByIdAsync(string userId, string followerId)
        { 
            await _followersRepository.DeleteFollowerByIdAsync(userId, followerId);

            return await _followersRepository.Complete();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserFollowers>> GetFollowersByIdAsync(string userId)
        {
            return await _followersRepository.GetFollowersByIdAsync(userId);
        }
    }
}
