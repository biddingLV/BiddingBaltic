import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesAddComponent } from './add.component';

describe('AddComponent', () => {
  let component: FeaturesAddComponent;
  let fixture: ComponentFixture<FeaturesAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FeaturesAddComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeaturesAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });
});
