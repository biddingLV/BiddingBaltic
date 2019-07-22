using Bidding.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Admin.Users.List
{
    public class UserListResponseModel : BaseListResponseModel
    {
        public List<UserListModel> Users { get; set; }

        public override bool IsReponseEmpty()
        {
            return !Users.Any();
        }
    }
}
