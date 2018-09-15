import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArrayDropdownComponent } from './array.component';

describe('ArrayComponent', () => {
  let component: ArrayDropdownComponent;
  let fixture: ComponentFixture<ArrayDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ArrayDropdownComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArrayDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });
});
