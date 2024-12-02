import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyListComponent } from './property-list.component';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('PropertyListComponent', () => {
  let component: PropertyListComponent;
  let fixture: ComponentFixture<PropertyListComponent>;
  let propertyServiceMock: any;
  let routerMock: any;

  beforeEach(async () => {
    propertyServiceMock = {
      getPropertiesWithPagination: jasmine.createSpy('getPropertiesWithPagination'),
      deleteProperty: jasmine.createSpy('deleteProperty')
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    await TestBed.configureTestingModule({
      imports: [PropertyListComponent],
      providers: [
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyListComponent);
    component = fixture.componentInstance;

    propertyServiceMock.getPropertiesWithPagination.and.returnValue(of({
      items: [
        {
          id: '1',
          title: 'Modern Apartment',
          description: 'A beautiful apartment in the city center.',
          type: 'APARTMENT',
          status: 'AVAILABLE',
          price: 100000,
          address: '123 Main St',
          area: 120,
          rooms: 3,
          bathrooms: 2,
          constructionYear: 2015,
          createdAt: new Date(),
          updatedAt: new Date(),
          userId: 'user123'
        },
        {
          id: '2',
          title: 'Luxury Villa',
          description: 'A spacious villa with a swimming pool.',
          type: 'HOUSE',
          status: 'SOLD',
          price: 500000,
          address: '456 Ocean Drive',
          area: 350,
          rooms: 5,
          bathrooms: 4,
          constructionYear: 2018,
          createdAt: new Date(),
          updatedAt: new Date(),
          userId: 'user456'
        }
      ],
      totalPages: 2
    }));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load properties on initialization', () => {
    expect(propertyServiceMock.getPropertiesWithPagination).toHaveBeenCalledWith(1, 2);
    expect(component.properties.length).toBe(2);
    expect(component.totalPages).toBe(2);
    expect(component.properties[0].title).toBe('Modern Apartment');
  });

  it('should navigate to create property page', () => {
    component.navigateToCreate();
    expect(routerMock.navigate).toHaveBeenCalledWith(['properties/create']);
  });

  it('should navigate to property details page', () => {
    const property = component.properties[0];
    component.viewDetails(property);
    expect(routerMock.navigate).toHaveBeenCalledWith(['properties/property-details', property.id]);
  });

  it('should handle error when deleting property', () => {
    const property = component.properties[0];
    spyOn(window, 'confirm').and.returnValue(true);
    propertyServiceMock.deleteProperty.and.returnValue(throwError(() => new Error('Delete error')));

    spyOn(window, 'alert');

    expect(propertyServiceMock.deleteProperty).toHaveBeenCalledWith(property.id);
    expect(window.alert).toHaveBeenCalledWith('Failed to delete property.');
  });

  it('should navigate to the next page', () => {
    component.pageNumber = 1;
    component.nextPage();
    expect(component.pageNumber).toBe(2);
    expect(propertyServiceMock.getPropertiesWithPagination).toHaveBeenCalledWith(2, 2);
  });

  it('should navigate to the previous page', () => {
    component.pageNumber = 2;
    component.previousPage();
    expect(component.pageNumber).toBe(1);
    expect(propertyServiceMock.getPropertiesWithPagination).toHaveBeenCalledWith(1, 2);
  });

  it('should not navigate to invalid pages', () => {
    component.pageNumber = 1;
    component.goToPage(0);
    expect(component.pageNumber).toBe(1);

    component.goToPage(3);
    expect(component.pageNumber).toBe(1);

    component.goToPage(2);
    expect(component.pageNumber).toBe(2);
    expect(propertyServiceMock.getPropertiesWithPagination).toHaveBeenCalledWith(2, 2);
  });
});
