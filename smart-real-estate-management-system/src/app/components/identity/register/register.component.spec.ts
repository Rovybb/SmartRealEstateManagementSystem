import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterComponent } from './register.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RegisterService } from '../../../services/identity/register.service';
import { of, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let registerServiceMock: any;

  beforeEach(async () => {
    registerServiceMock = {
      register: jasmine.createSpy('register')
    };

    await TestBed.configureTestingModule({
      imports: [RegisterComponent, ReactiveFormsModule, CommonModule],
      providers: [
        FormBuilder,
        { provide: RegisterService, useValue: registerServiceMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the RegisterComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with default values', () => {
    const form = component.registerForm;
    expect(form).toBeDefined();
    expect(form.controls['email'].value).toBe('');
    expect(form.controls['password'].value).toBe('');
    expect(form.controls['confirmPassword'].value).toBe('');
  });

  it('should validate the form fields correctly', () => {
    const form = component.registerForm;

    form.controls['email'].setValue('');
    form.controls['password'].setValue('short');
    form.controls['confirmPassword'].setValue('different');

    expect(form.valid).toBeFalse();
    expect(form.controls['email'].valid).toBeFalse();
    expect(form.controls['password'].valid).toBeFalse();
    expect(form.controls['confirmPassword'].valid).toBeTrue(); // Since confirmPassword is required only by custom validator

    // Valid inputs
    form.controls['email'].setValue('test@example.com');
    form.controls['password'].setValue('password123');
    form.controls['confirmPassword'].setValue('password123');

    expect(form.valid).toBeTrue();
  });

  it('should fail validation when passwords do not match', () => {
    const form = component.registerForm;
    form.controls['email'].setValue('test@example.com');
    form.controls['password'].setValue('password123');
    form.controls['confirmPassword'].setValue('differentPassword');

    expect(form.errors).toEqual({ mustMatch: true });
    expect(form.valid).toBeFalse();
  });

  it('should submit the form if valid', () => {
    const form = component.registerForm;
    form.setValue({
      email: 'test@example.com',
      password: 'password123',
      confirmPassword: 'password123'
    });

    registerServiceMock.register.and.returnValue(of({}));

    component.onSubmit();

    expect(form.valid).toBeTrue();
    expect(registerServiceMock.register).toHaveBeenCalledWith('test@example.com', 'password123');
    expect(component.errorMessage).toBeNull();
  });

  it('should not submit the form if invalid', () => {
    const form = component.registerForm;
    form.controls['email'].setValue('');
    form.controls['password'].setValue('');

    component.onSubmit();

    expect(form.valid).toBeFalse();
    expect(registerServiceMock.register).not.toHaveBeenCalled();
    expect(component.errorMessage).toBe('Please fix validation errors before submitting.');
  });

  it('should handle registration errors', () => {
    const form = component.registerForm;
    form.setValue({
      email: 'test@example.com',
      password: 'password123',
      confirmPassword: 'password123'
    });

    registerServiceMock.register.and.returnValue(throwError(() => new Error('Registration error')));

    component.onSubmit();

    expect(registerServiceMock.register).toHaveBeenCalledWith('test@example.com', 'password123');
    expect(component.errorMessage).toBe('Registration failed. Please try again.');
  });
});
