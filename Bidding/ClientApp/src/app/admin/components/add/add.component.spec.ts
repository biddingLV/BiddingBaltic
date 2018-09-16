import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionAddComponent } from './add.component';

describe('AddComponent', () => {
  let component: AuctionAddComponent;
  let fixture: ComponentFixture<AuctionAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuctionAddComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuctionAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });
});
