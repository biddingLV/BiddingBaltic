import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComingSoonNavbarComponent } from './coming-soon-navbav.component';

describe('NavbarComponent', () => {
  let component: ComingSoonNavbarComponent;
  let fixture: ComponentFixture<ComingSoonNavbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ComingSoonNavbarComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComingSoonNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
