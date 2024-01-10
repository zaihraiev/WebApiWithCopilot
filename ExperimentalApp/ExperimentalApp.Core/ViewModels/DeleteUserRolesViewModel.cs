using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalApp.Core.ViewModels
{
    /// <summary>
    /// Represents the view model for deleting roles.
    /// </summary>
    public class DeleteUserRolesViewModel
    {
        /// <summary>
        /// Represents the user whose roles will be removed
        /// </summary>
        [Required]
        public string? UserId { get; set; }

        /// <summary>
        /// Represents the list of roles to be removed
        /// </summary>
        [Required]
        public List<string>? RolesIds { get; set; }
    }
}
