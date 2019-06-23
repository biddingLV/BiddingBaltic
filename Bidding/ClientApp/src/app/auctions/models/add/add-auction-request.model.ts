namespace Auctions {
  export interface AddAuctionRequestModel {
    auctionTopCategoryId: number;

    /** Can be nullable because 'Mantu kopa' doesnt have sub-category */
    auctionSubCategoryId?: number;
    auctionName: string;
    auctionStartingPrice: number;
    vehicleAuction?: Auctions.VehicleAuctionModel;
    itemAuction?: Auctions.ItemAuctionModel;
    propertyAuction?: Auctions.PropertyAuctionModel;
    aboutAuction: Auctions.AboutAuctionModel;
  }
}
