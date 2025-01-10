import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyUpdateComponent } from './property-update.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { Router, ActivatedRoute } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('PropertyUpdateComponent', () => {
  let component: PropertyUpdateComponent;
  let fixture: ComponentFixture<PropertyUpdateComponent>;
  let propertyServiceMock: any;
  let routerMock: any;
  let activatedRouteMock: any;

  beforeEach(async () => {
    propertyServiceMock = {
      getPropertyById: jasmine.createSpy('getPropertyById').and.returnValue(of({})), // Fix: Return an observable
      updateProperty: jasmine.createSpy('updateProperty')
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
      imports: [PropertyUpdateComponent, ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with default values', () => {
    const form = component.propertyForm;
    expect(form).toBeDefined();
    expect(form.value).toEqual({
      id: '1',
      title: '',
      description: '',
      status: '',
      type: '',
      price: null,
      address: '',
      area: null,
      rooms: null,
      bathrooms: null,
      constructionYear: null,
      userId: ''
    });
  });

  it('should load property data on init', () => {
    const mockProperty = {
      id: '1',
      title: 'Test Property',
      description: 'Description of test property',
      status: 'AVAILABLE',
      type: 'HOUSE',
      price: 200000,
      address: '123 Test St',
      area: 150,
      rooms: 3,
      bathrooms: 2,
      constructionYear: 2000,
      userId: 'user123'
    };

    propertyServiceMock.getPropertyById.and.returnValue(of(mockProperty));

    component.ngOnInit();

    expect(propertyServiceMock.getPropertyById).toHaveBeenCalledWith('1');
    expect(component.propertyForm.value).toEqual(mockProperty);
  });

  it('should handle form submission', () => {
    const form = component.propertyForm;
    form.setValue({
      id: '1',
      title: 'Updated Property',
      description: 'Updated description',
      status: 'AVAILABLE',
      type: 'HOUSE',
      price: 300000,
      address: '456 Updated St',
      area: 200,
      rooms: 5,
      bathrooms: 3,
      constructionYear: 2020,
      userId: 'user123'
    });

    propertyServiceMock.updateProperty.and.returnValue(of({}));

    component.onSubmit();

    expect(form.valid).toBeTrue();
    expect(propertyServiceMock.updateProperty).toHaveBeenCalledWith('1', form.value);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/properties']);
  });

  it('should handle submission errors', () => {
    const form = component.propertyForm;
    form.setValue({
      id: '1',
      title: 'Updated Property',
      description: 'Updated description',
      status: 'AVAILABLE',
      type: 'HOUSE',
      price: 300000,
      address: '456 Updated St',
      area: 200,
      rooms: 5,
      bathrooms: 3,
      constructionYear: 2020,
      userId: 'user123'
    });

    propertyServiceMock.updateProperty.and.returnValue(throwError(() => new Error('Update error')));

    component.onSubmit();

    expect(propertyServiceMock.updateProperty).toHaveBeenCalledWith('1', form.value);
    expect(component.errorMessage).toBe('Error updating property. Please try again.');
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });

  it('should handle validation errors on submission', () => {
    const form = component.propertyForm;
    form.controls['title'].setValue('');
    form.controls['description'].setValue('');

    component.onSubmit();

    expect(form.valid).toBeFalse();
    expect(component.errorMessage).toBe('Please fix validation errors before submitting.');
    expect(propertyServiceMock.updateProperty).not.toHaveBeenCalled();
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });
});
