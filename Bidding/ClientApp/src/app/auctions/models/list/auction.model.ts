export class AuctionModel {
  constructor(
    public type: Date,
    public price: Date,
    public brand: boolean,
    public Description?: string,
    public _id?: string,
  ) { }
}
