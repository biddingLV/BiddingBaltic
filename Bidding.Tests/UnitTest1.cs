using System;
using Xunit;
using Moq;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Services.Auctions;
using Bidding.Services.Shared.Permissions;
using Bidding.Repositories.Auctions;
using System.Collections.Generic;

namespace Bidding.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetActiveAuctions_1()
        {
            AuctionListRequestModel request = new AuctionListRequestModel()
            {
                SortingDirection = "asc",
                OffsetStart = 0,
                OffsetEnd = 10,
                CurrentPage = 1,
                SortByColumn = "AuctionName"
            };

            //var mockedAuctionService = new Mock<IAuctionService>();
            var mockedAuctionRepository = new Mock<IAuctionRepository>();
            mockedAuctionRepository
                .Setup(x => x.GetActiveAuctions(new AuctionListRequestModel() { }, 0, 10, DateTime.UtcNow))
                .Returns(It.IsAny<IEnumerable<AuctionListItemModel>>());

            var mockedPermissionService = new Mock<IPermissionService>();

            var auctionService = new AuctionService(mockedAuctionRepository.Object, mockedPermissionService.Object);

            var result = auctionService.GetActiveAuctions(request);

            //Assert.Equal();
        }
    }
}
