import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { Property } from '../../models/property.model'; // Import modelul Property
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
  property: Property | null = null;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const propertyId = this.route.snapshot.paramMap.get('id'); // Get `id` directly as a string

    if (propertyId) {
      this.propertyService.getPropertyById(propertyId).subscribe({
        next: (data) => {
          this.property = data;
        },
        error: (error) => {
          this.errorMessage = 'Error fetching property details. Please try again.';
          console.error('Fetch error:', error);
          this.router.navigate(['/properties']);
        }
      });
    } else {
      this.errorMessage = 'Property ID is missing!';
      this.router.navigate(['/properties']);
    }
  }

  goBack(): void {
    this.router.navigate(['/properties']);
  }

  navigateToUpdate(property: Property) {
    this.router.navigate(['properties/update/' + property.id]);
  }

  deleteProperty(property: Property): void {
    if (confirm('Are you sure you want to delete this property?')) {
      const propertyId = property.id;
      this.propertyService.deleteProperty(propertyId).subscribe({
        next: () => {
          this.router.navigate(['/properties']);
        },
        error: (err) => {
          console.error('Error deleting property:', err);
          alert('Failed to delete property.');
        }
      });
    }
  }

  // Convert numeric status to string representation
  getPropertyStatus(status: any): string {
    switch (status) {
      case 0:
        return 'AVAILABLE';
      case 1:
        return 'SOLD';
      case 2:
        return 'RENTED';
      default:
        return 'UNKNOWN';
    }
  }

  getPropertyType(type: any): string {
    switch (type) {
      case 0:
        return 'HOUSE';
      case 1:
        return 'APARTMENT';
      case 2:
        return 'LAND';
      case 3:
        return 'COMMERCIAL';
      default:
        return 'UNKNOWN';
    }
  }
}
