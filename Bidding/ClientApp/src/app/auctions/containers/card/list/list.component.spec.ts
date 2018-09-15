import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AuctionCardListComponent } from './list.component';

describe('AuctionCardListComponent', () => {
  let component: AuctionCardListComponent;
  let fixture: ComponentFixture<AuctionCardListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuctionCardListComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuctionCardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
