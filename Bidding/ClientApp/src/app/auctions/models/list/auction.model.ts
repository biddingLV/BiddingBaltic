export class AuctionModel {
  constructor(
    // make this CAPS! check style guide!
    public startDate: Date,
    public endDate: Date,
    public type: string,
    public price: number,
    public brand: string,
    public description: string,
    public id?: number,
  ) { }
}
