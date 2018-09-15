import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionsTableComponent } from './auctions.component';

describe('AuctionsTableComponent', () => {
  let component: AuctionsTableComponent;
  let fixture: ComponentFixture<AuctionsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuctionsTableComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuctionsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
