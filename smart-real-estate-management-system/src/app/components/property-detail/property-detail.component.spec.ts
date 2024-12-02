import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyDetailComponent } from './property-detail.component';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

describe('PropertyDetailComponent', () => {
  let component: PropertyDetailComponent;
  let fixture: ComponentFixture<PropertyDetailComponent>;
  let propertyServiceMock: any;
  let routerMock: any;
  let activatedRouteMock: any;

  beforeEach(async () => {
    propertyServiceMock = {
      getPropertyById: jasmine.createSpy('getPropertyById'),
      deleteProperty: jasmine.createSpy('deleteProperty')
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    activatedRouteMock = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.returnValue('1')
        }
      }
    };

    await TestBed.configureTestingModule({
      imports: [PropertyDetailComponent, CommonModule],
      providers: [
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyDetailComponent);
    component = fixture.componentInstance;

    // Mocking the data returned from the service
    propertyServiceMock.getPropertyById.and.returnValue(of({
      id: '1',
      title: 'Modern Apartment',
      description: 'A beautiful apartment in the city center.',
      type: 0,  // Set to 0 for 'HOUSE'
      status: 0,  // Set to 0 for 'AVAILABLE'
      price: 100000,
      address: '123 Main St',
      area: 120,
      rooms: 3,
      bathrooms: 2,
      constructionYear: 2015,
      createdAt: new Date(),
      updatedAt: new Date(),
      userId: 'user123'
    }));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load property details on initialization', () => {
    expect(propertyServiceMock.getPropertyById).toHaveBeenCalledWith('1');
    expect(component.property).toBeTruthy();
    expect(component.property?.title).toBe('Modern Apartment');
  });

  it('should handle error when property details are not found', () => {
    propertyServiceMock.getPropertyById.and.returnValue(throwError(() => new Error('Property not found')));
    component.ngOnInit();
    expect(component.errorMessage).toBe('Error fetching property details. Please try again.');
    expect(routerMock.navigate).toHaveBeenCalledWith(['/properties']);
  });

  it('should navigate to update property page', () => {
    const property = component.property!;
    component.navigateToUpdate(property);
    expect(routerMock.navigate).toHaveBeenCalledWith(['properties/update/' + property.id]);
  });
  

  it('should confirm before deleting property and show error if deletion fails', () => {
    const property = component.property!;
    spyOn(window, 'confirm').and.returnValue(true);
    propertyServiceMock.deleteProperty.and.returnValue(throwError(() => new Error('Delete error')));
    spyOn(window, 'alert');

    component.deleteProperty(property);

    expect(propertyServiceMock.deleteProperty).toHaveBeenCalledWith(property.id);
    expect(window.alert).toHaveBeenCalledWith('Failed to delete property.');
  });

  it('should navigate back to properties list', () => {
    component.goBack();
    expect(routerMock.navigate).toHaveBeenCalledWith(['/properties']);
  });

  it('should display correct property status', () => {
    const status = component.getPropertyStatus(0);
    expect(status).toBe('AVAILABLE');
  });

  it('should display correct property type', () => {
    const type = component.getPropertyType(0);  // Should match 'HOUSE' (0)
    expect(type).toBe('HOUSE');
  });

  it('should handle invalid status and type', () => {
    const invalidStatus = component.getPropertyStatus(99);
    const invalidType = component.getPropertyType(99);
    expect(invalidStatus).toBe('UNKNOWN');
    expect(invalidType).toBe('UNKNOWN');
  });
});
