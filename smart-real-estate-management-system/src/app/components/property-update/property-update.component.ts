import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-property-update',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './property-update.component.html',
  styleUrls: ['./property-update.component.css']
})
export class PropertyUpdateComponent implements OnInit {
  propertyForm: FormGroup;
  errorMessage: string | null = null; // To store error messages

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private router: Router
  ) {
    this.propertyForm = this.fb.group({
      id: ['', Validators.required],
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      status: ['', [Validators.required]],
      type: ['', [Validators.required]],
      price: [null, [Validators.required, Validators.min(0.01)]],
      address: ['', [Validators.required, Validators.maxLength(200)]],
      area: [null, [Validators.required, Validators.min(0.01)]],
      rooms: [null, [Validators.required, Validators.min(0)]],
      bathrooms: [null, [Validators.required, Validators.min(0)]],
      constructionYear: [null, [Validators.required, Validators.min(1501)]],
      userId: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.errorMessage = null; // Reset error message

    if (this.propertyForm.valid) {
      this.propertyService
        .updateProperty(this.propertyForm.value.id, this.propertyForm.value)
        .subscribe({
          next: () => {
            this.router.navigate(['/properties']);
          },
          error: (error) => {
            this.errorMessage = 'Error updating property. Please try again.';
            console.error('Update error:', error);
          }
        });
    } else {
      this.errorMessage = 'Please fix validation errors before submitting.';
    }
  }
}
