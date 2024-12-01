import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-property-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './property-create.component.html',
  styleUrls: ['./property-create.component.css']
})
export class PropertyCreateComponent implements OnInit {
  propertyForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private router: Router
  ) {
    this.propertyForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      status: ['', [Validators.required]], // Add further validation if needed for enum
      type: ['', [Validators.required]], // Add further validation if needed for enum
      price: [null, [Validators.required, Validators.min(0.01)]],
      address: ['', [Validators.required, Validators.maxLength(200)]],
      area: [null, [Validators.required, Validators.min(0.01)]],
      rooms: [null, [Validators.required, Validators.min(0)]],
      bathrooms: [null, [Validators.required, Validators.min(0)]],
      constructionYear: [null, [Validators.required, Validators.min(1501)]],
      userId: ['', Validators.required] // Assuming userId is provided
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.propertyForm.valid) {
      this.propertyService.createProperty(this.propertyForm.value).subscribe(() => {
        this.router.navigate(['/properties']);
      });
    }
  }
}
