export class AuctionModel {
  constructor(
    public startDate: Date,
    public endDate: Date,
    public type: string,
    public price: number,
    public brand: string,
    public description: string,
    public _id?: number,
  ) { }
}
