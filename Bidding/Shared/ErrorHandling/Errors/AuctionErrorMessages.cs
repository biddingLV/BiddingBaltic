using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.ErrorHandling.Errors
{
    public enum AuctionErrorMessages
    {
        [Description("Auctions information missing")]
        MissingAuctionsInformation,

        [Description("Could not fetch auction list")]
        CouldNotFetchAuctionList,

        [Description("Could not fetch auction creator list")]
        CouldNotFetchAuctionCreatorList,

        [Description("Could not create auction")]
        CouldNotCreateAuction,

        [Description("Could not update auction")]
        CouldNotUpdateAuction,

        [Description("Could not delete auction")]
        CouldNotDeleteAuction,

        [Description("Could not delete, because it is not active anymore")]
        NotActiveAuction,

        [Description("Top category ids are incorrect")]
        TopCategoryIdsNotCorrect,

        [Description("Incorrect auction or doesn't exist")]
        IncorrectAuction,

        [Description("Incorrect auction status or doesn't exist")]
        MissingRequiredAuctionStatus
    }
}
