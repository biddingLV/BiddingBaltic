import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GarageAuctionsComponent } from './garage-auctions.component';

describe('GarageAuctionsComponent', () => {
  let component: GarageAuctionsComponent;
  let fixture: ComponentFixture<GarageAuctionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GarageAuctionsComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GarageAuctionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
