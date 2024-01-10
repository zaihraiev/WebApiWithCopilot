using ExperimentalApp.Core.Models;
using ExperimentalApp.Core.Models.Identity;
using ExperimentalApp.DataAccessLayer.DataDbContext;
using ExperimentalApp.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExperimentalApp.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// </summary>
    public class FollowersRepository : IFollowersRepository
    {
        private readonly DvdRentalContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowersRepository"/> class.
        /// </summary>
        /// <param name="userManager">Service for managing users</param>
        public FollowersRepository(DvdRentalContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void AddFollower(UserFollowers follower)
        {
            _context.UserFollowers.Add(follower);
        }

        /// <inheritdoc/>
        public async Task<bool> DoesUserHaveFollowerAsync(string userId, string followerId)
        {
            return await _context.UserFollowers.AnyAsync(x => x.FollowerId == followerId && x.FollowingId == userId);
        }

        /// <inheritdoc/>
        public async Task DeleteFollowerByIdAsync(string userId, string followerId)
        {
            var follower = await _context.UserFollowers.FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowingId == userId);

            if (follower != null)
            {
                _context.UserFollowers.Remove(follower);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserFollowers>> GetFollowersByIdAsync(string userId)
        {
            return await _context.UserFollowers.Where(x => x.FollowingId == userId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
