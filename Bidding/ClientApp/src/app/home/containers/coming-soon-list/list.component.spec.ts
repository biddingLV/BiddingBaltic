import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ComingSoonListComponent } from './list.component';

describe('ComingSoonListComponent', () => {
  let component: ComingSoonListComponent;
  let fixture: ComponentFixture<ComingSoonListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ComingSoonListComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComingSoonListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
