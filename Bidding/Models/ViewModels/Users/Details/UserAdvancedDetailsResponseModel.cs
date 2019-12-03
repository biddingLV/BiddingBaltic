using Bidding.Models.ViewModels.Users.Shared;
using System;
using System.Collections.Generic;

namespace Bidding.Models.ViewModels.Users.Details
{
    public class UserAdvancedDetailsResponseModel : UserBasicDetailsResponseModel
    {
        /// <summary>
        /// Selected user assigned role id.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// All possible roles in the system.
        /// </summary>
        public List<RoleItemModel> Roles { get; set; }
        public DateTime? SubscriptionTill { get; set; }
    }
}
