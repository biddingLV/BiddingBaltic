import { GdprModule } from './partners.module';

describe('GdprModule', () => {
  let gdprModule: GdprModule;

  beforeEach(() => {
    gdprModule = new GdprModule();
  });

  it('should create an instance', () => {
    expect(gdprModule).toBeTruthy();
  });
});
