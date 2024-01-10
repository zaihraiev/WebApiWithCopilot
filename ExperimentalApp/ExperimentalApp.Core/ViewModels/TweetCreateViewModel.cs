using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalApp.Core.ViewModels
{
    /// <summary>
    /// Represents the view model for tweet creation.
    /// </summary>
    public class TweetCreateViewModel
    {
        /// <summary>
        /// Represents the text of the tweet.
        /// </summary>
        [Required]
        public string? Text { get; set; }

        /// <summary>
        /// Represents the tags associated with the tweet.
        /// </summary>
        [Required]
        public List<string>? Tags { get; set; }
    }
}
