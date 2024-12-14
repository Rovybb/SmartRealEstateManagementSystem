import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterService } from '../../../services/identity/register.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService
  ) {
    this.registerForm = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(8)]]
      },
      { validator: this.passwordMatchValidator }  // Validare personalizată
    );
  }

  ngOnInit(): void {}

  // Validator pentru confirmarea parolei
  passwordMatchValidator(form: FormGroup): { [key: string]: boolean } | null {
    const password = form.get('password');
    const confirmPassword = form.get('confirmPassword');

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { mustMatch: true };
    }
    return null;
  }

  onSubmit(): void {
    this.errorMessage = null;

    if (this.registerForm.valid) {
      const { email, password } = this.registerForm.value;

      this.registerService.register(email, password).subscribe({
        next: () => {
          console.log('User registered');
        },
        error: (error) => {
          this.errorMessage = 'Registration failed. Please try again.';
          console.error('Registration error:', error);
        }
      });
    } else {
      this.errorMessage = 'Please fix validation errors before submitting.';
    }
  }
}
