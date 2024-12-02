import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyUpdateComponent } from './property-update.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('PropertyUpdateComponent', () => {
  let component: PropertyUpdateComponent;
  let fixture: ComponentFixture<PropertyUpdateComponent>;
  let propertyServiceMock: any;
  let routerMock: any;

  beforeEach(async () => {
    propertyServiceMock = {
      updateProperty: jasmine.createSpy('updateProperty')
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    await TestBed.configureTestingModule({
      imports: [PropertyUpdateComponent, ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock }
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
      id: '',
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

    form.controls['id'].setValue('');
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
    expect(form.controls['id'].valid).toBeFalse();
    expect(form.controls['title'].valid).toBeFalse();
    expect(form.controls['description'].valid).toBeFalse();
    expect(form.controls['price'].valid).toBeFalse();
    expect(form.controls['constructionYear'].valid).toBeFalse();
  });

  it('should submit the form if valid', () => {
    const form = component.propertyForm;
    form.setValue({
      id: '1',
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

    propertyServiceMock.updateProperty.and.returnValue(of({}));

    component.onSubmit();

    expect(form.valid).toBeTrue();
    expect(propertyServiceMock.updateProperty).toHaveBeenCalledWith('1', form.value);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/properties']);
  });

  it('should not submit the form if invalid', () => {
    const form = component.propertyForm;
    form.controls['title'].setValue('');
    form.controls['description'].setValue('');

    component.onSubmit();

    expect(form.valid).toBeFalse();
    expect(propertyServiceMock.updateProperty).not.toHaveBeenCalled();
    expect(routerMock.navigate).not.toHaveBeenCalled();
  });

  it('should handle errors during submission', () => {
    const form = component.propertyForm;
    form.setValue({
      id: '1',
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
