import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyCreateComponent } from './property-create.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('PropertyCreateComponent', () => {
  let component: PropertyCreateComponent;
  let fixture: ComponentFixture<PropertyCreateComponent>;
  let propertyServiceMock: any;
  let routerMock: any;

  beforeEach(async () => {
    propertyServiceMock = {
      createProperty: jasmine.createSpy('createProperty')
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    await TestBed.configureTestingModule({
      imports: [PropertyCreateComponent, ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyCreateComponent);
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

  it('should validate the form fields correctly', () => {
    const form = component.propertyForm;

    form.controls['title'].setValue('');
    form.controls['description'].setValue('');
    form.controls['status'].setValue('');
    form.controls['type'].setValue('');
    form.controls['price'].setValue(-1);
    form.controls['address'].setValue('');
    form.controls['area'].setValue(-1);
    form.controls['rooms'].setValue(-1);
    form.controls['bathrooms'].setValue(-1);
    form.controls['constructionYear'].setValue(1000);
    form.controls['userId'].setValue('');

    expect(form.valid).toBeFalse();
    expect(form.controls['title'].valid).toBeFalse();
    expect(form.controls['description'].valid).toBeFalse();
    expect(form.controls['price'].valid).toBeFalse();
    expect(form.controls['constructionYear'].valid).toBeFalse();
  });

  it('should submit the form if valid', () => {
    const form = component.propertyForm;
    form.setValue({
      title: 'Test Property',
      description: 'This is a test property.',
      status: 'AVAILABLE',
      type: 'HOUSE',
      price: 100000,
      address: '123 Test St',
      area: 150,
      rooms: 4,
      bathrooms: 2,
      constructionYear: 2000,
      userId: 'user123'
    });

    propertyServiceMock.createProperty.and.returnValue(of({}));

    component.onSubmit();

    expect(form.valid).toBeTrue();
    expect(propertyServiceMock.createProperty).toHaveBeenCalledWith(form.value);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/properties']);
  });

  it('should not submit the form if invalid', () => {
    const form = component.propertyForm;
    form.controls['title'].setValue('');
    form.controls['description'].setValue('');

    component.onSubmit();

    expect(form.valid).toBeFalse();
    expect(propertyServiceMock.createProperty).not.toHaveBeenCalled();
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });

  it('should handle validation errors on submission', () => {
    const form = component.propertyForm;
    form.controls['title'].setValue('');
    form.controls['description'].setValue('');

    component.onSubmit();

    expect(form.valid).toBeFalse();
    expect(component.errorMessage).toBe('Please fix validation errors before submitting.');
    expect(propertyServiceMock.createProperty).not.toHaveBeenCalled();
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });

  it('should handle errors during submission', () => {
    const form = component.propertyForm;
    form.setValue({
      title: 'Test Property',
      description: 'This is a test property.',
      status: 'AVAILABLE',
      type: 'HOUSE',
      price: 100000,
      address: '123 Test St',
      area: 150,
      rooms: 4,
      bathrooms: 2,
      constructionYear: 2000,
      userId: 'user123'
    });

    propertyServiceMock.createProperty.and.returnValue(throwError(() => new Error('Create error')));

    component.onSubmit();

    expect(propertyServiceMock.createProperty).toHaveBeenCalledWith(form.value);
    expect(component.errorMessage).toBe('Error creating property. Please try again.');
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });
});
