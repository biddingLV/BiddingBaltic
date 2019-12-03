using Bidding.Models.ViewModels.BaseModels;
using System.Collections.Generic;
using System.Linq;

namespace Bidding.Models.ViewModels.Admin.Users.List
{
    public class UserListResponseModel : BaseListResponseModel
    {
        public List<UserListItemModel> Users { get; set; }

        public override bool IsReponseEmpty()
        {
            return !Users.Any();
        }
    }
}
