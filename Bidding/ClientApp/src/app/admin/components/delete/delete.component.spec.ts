import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionDeleteComponent } from './delete.component';

describe('AddComponent', () => {
  let component: AuctionDeleteComponent;
  let fixture: ComponentFixture<AuctionDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuctionDeleteComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuctionDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });
});
