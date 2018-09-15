import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SubscribeEmailComponent } from './email.component';

describe('SubscribeEmailComponent', () => {
  let component: SubscribeEmailComponent;
  let fixture: ComponentFixture<SubscribeEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SubscribeEmailComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscribeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
