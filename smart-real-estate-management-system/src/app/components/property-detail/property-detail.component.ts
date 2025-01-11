import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../services/property.service';
import { Property } from '../../models/property.model'; // Import modelul Property
import { CommonModule } from '@angular/common';
import { LoginService } from '../../services/identity/login.service'; // Import LoginService
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-property-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
  property: Property | null = null;
  errorMessage: string | null = null;

  // Inquiry Pop-Up Properties
  isInquiryPopupOpen: boolean = false;
  inquiryMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private router: Router,
    private loginService: LoginService, // Adaugă LoginService

  ) {}

  ngOnInit(): void {
    const propertyId = this.route.snapshot.paramMap.get('id');
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

  // Inquiry Methods
  openInquiryPopup(): void {
    this.isInquiryPopupOpen = true;
  }

  closeInquiryPopup(): void {
    this.isInquiryPopupOpen = false;
    this.inquiryMessage = ''; // Reset message
  }

  sendInquiry(): void {
    if (!this.inquiryMessage.trim()) {
      alert('Please write a message before sending.');
      return;
    }
  
    const clientId = this.loginService.getUserId(); // Obține clientId din LoginService
    if (!clientId) {
      alert('You need to be logged in to send an inquiry.');
      return;
    }
  
    if (this.property && this.property.id && this.property.userId) {
      const agentId = this.property.userId; // Owner-ul proprietății
      const status = 0; // Status implicit
  
      this.propertyService
        .sendInquiry(this.property.id, this.inquiryMessage, status, agentId, clientId)
        .subscribe({
          next: () => {
            alert('Your inquiry has been sent successfully!');
            this.closeInquiryPopup();
          },
          error: (err) => {
            console.error('Error sending inquiry:', err);
            alert('Failed to send inquiry. Please try again.');
          }
        });
    } else {
      alert('Property information is missing. Unable to send inquiry.');
    }
  }

  // Other Methods (unchanged)
  goBack(): void {
    this.router.navigate(['/properties']);
  }

  navigateToUpdate(property: Property) {
    this.router.navigate(['properties/update/' + property.id]);
  }

  deleteProperty(property: Property): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(property.id).subscribe({
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