import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BlogWidgetComponent } from './widget.component';

describe('BlogWidgetComponent', () => {
  let component: BlogWidgetComponent;
  let fixture: ComponentFixture<BlogWidgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BlogWidgetComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogWidgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
