import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ComingSoonSurveyComponent } from './survey.component';

describe('ComingSoonSurveyComponent', () => {
  let component: ComingSoonSurveyComponent;
  let fixture: ComponentFixture<ComingSoonSurveyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ComingSoonSurveyComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComingSoonSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
