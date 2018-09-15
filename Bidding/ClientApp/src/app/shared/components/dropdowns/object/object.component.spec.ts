import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectDropdownComponent } from './object.component';

describe('ObjectComponent', () => {
  let component: ObjectDropdownComponent;
  let fixture: ComponentFixture<ObjectDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ObjectDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ObjectDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
    .toBeTruthy();
  });
});
