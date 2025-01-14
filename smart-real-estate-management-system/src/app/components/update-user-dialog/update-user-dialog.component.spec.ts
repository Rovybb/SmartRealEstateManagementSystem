import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavbarHomeComponent } from '../navbar-home/navbar-home.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Property } from '../../models/property.model';
import { PropertyListComponent } from '../property-list/property-list.component';

describe('PropertyListComponent', () => {
  let component: PropertyListComponent;
  let fixture: ComponentFixture<PropertyListComponent>;
  let propertyService: jasmine.SpyObj<PropertyService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    const propertyServiceSpy = jasmine.createSpyObj('PropertyService', ['getPropertiesWithPagination', 'getPropertyById']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [PropertyListComponent],
      imports: [FontAwesomeModule, NavbarHomeComponent, CommonModule, FormsModule],
      providers: [
        { provide: PropertyService, useValue: propertyServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyListComponent);
    component = fixture.componentInstance;
    propertyService = TestBed.inject(PropertyService) as jasmine.SpyObj<PropertyService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load properties on initialization', () => {
    const mockData = {
      items: [
        { id: '1', title: 'Property 1', imageUrls: [] },
        { id: '2', title: 'Property 2', imageUrls: [] },
      ],
      totalPages: 2,
    };

    propertyService.getPropertiesWithPagination.and.returnValue(of(mockData));

    component.ngOnInit();

    expect(propertyService.getPropertiesWithPagination).toHaveBeenCalledWith(1, 2, {});
    expect(component.properties.length).toBe(2);
    expect(component.totalPages).toBe(2);
  });

  it('should handle error when loading properties fails', () => {
    spyOn(console, 'error');

    propertyService.getPropertiesWithPagination.and.returnValue(throwError(() => new Error('Test Error')));

    component.ngOnInit();

    expect(console.error).toHaveBeenCalledWith('Eroare la preluarea proprietăților:', jasmine.any(Error));
    expect(component.properties).toEqual([]);
  });

  it('should apply filters and reload properties', () => {
    spyOn(component, 'loadProperties');

    component.applyFilters();

    expect(component.pageNumber).toBe(1);
    expect(component.loadProperties).toHaveBeenCalled();
  });

  it('should reset filters and reload properties', () => {
    spyOn(component, 'loadProperties');

    component.resetFilters();

    expect(component.filters).toEqual({ title: null, price_min: null, price_max: null, description: null });
    expect(component.pageNumber).toBe(1);
    expect(component.loadProperties).toHaveBeenCalled();
  });

  it('should navigate to the create property page', () => {
    component.navigateToCreate();

    expect(router.navigate).toHaveBeenCalledWith(['properties/create']);
  });

  it('should navigate to property details', () => {
    expect(router.navigate).toHaveBeenCalledWith(['/properties/property-details', '1']);
  });

  it('should update page number and reload properties when navigating to a page', () => {
    spyOn(component, 'loadProperties');

    component.totalPages = 5;
    component.goToPage(3);

    expect(component.pageNumber).toBe(3);
    expect(component.loadProperties).toHaveBeenCalled();
  });

  it('should get property type', () => {
    expect(component.getPropertyType(0)).toBe('house');
    expect(component.getPropertyType(1)).toBe('apartment');
    expect(component.getPropertyType(2)).toBe('land');
    expect(component.getPropertyType(3)).toBe('commercial');
    expect(component.getPropertyType(99)).toBe('unknown');
  });

  it('should get property status', () => {
    expect(component.getPropertyStatus(0)).toBe('available');
    expect(component.getPropertyStatus(1)).toBe('sold');
    expect(component.getPropertyStatus(2)).toBe('rented');
    expect(component.getPropertyStatus(99)).toBe('unknown');
  });
});