using Bidding.Models.ViewModels.BaseModels;
using System.Collections.Generic;
using System.Linq;

namespace Bidding.Models.ViewModels.Auctions.List
{
    public class AuctionListResponseModel : BaseListResponseModel
    {
        public List<AuctionListItemModel> Auctions { get; set; }

        public override bool IsReponseEmpty()
        {
            return !Auctions.Any();
        }
    }
}
