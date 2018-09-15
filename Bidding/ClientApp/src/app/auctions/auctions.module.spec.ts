import { AuctionsModule } from './auctions.module';

describe('AuctionsModule', () => {
  let auctionsModule: AuctionsModule;

  beforeEach(() => {
    auctionsModule = new AuctionsModule();
  });

  it('should create an instance', () => {
    expect(auctionsModule).toBeTruthy();
  });
});
