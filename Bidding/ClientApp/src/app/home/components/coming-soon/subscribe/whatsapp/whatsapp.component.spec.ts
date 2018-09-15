import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SubscribeWhatsappComponent } from './whatsapp.component';

describe('SubscribeEmailComponent', () => {
  let component: SubscribeWhatsappComponent;
  let fixture: ComponentFixture<SubscribeWhatsappComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SubscribeWhatsappComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscribeWhatsappComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
