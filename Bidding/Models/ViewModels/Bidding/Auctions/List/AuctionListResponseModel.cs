using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.DatabaseModels.Bidding;
using Bidding.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bidding.Models.ViewModels.Bidding.Auctions.List
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
