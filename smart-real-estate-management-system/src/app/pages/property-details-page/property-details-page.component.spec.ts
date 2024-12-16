import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyDetailsPageComponent } from './property-details-page.component';

describe('PropertyDetailsPageComponent', () => {
  let component: PropertyDetailsPageComponent;
  let fixture: ComponentFixture<PropertyDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PropertyDetailsPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PropertyDetailsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
