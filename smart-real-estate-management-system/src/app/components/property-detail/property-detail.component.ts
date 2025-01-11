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

  images: string[] = []; // Variabilă pentru a stoca imaginile proprietății
  uploading: boolean = false; // Indicator pentru încărcarea imaginilor

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private router: Router,
    private loginService: LoginService // Adaugă LoginService
  ) {}

  ngOnInit(): void {
    const propertyId = this.route.snapshot.paramMap.get('id');
    if (propertyId) {
      this.propertyService.getPropertyById(propertyId).subscribe({
        next: (data) => {
          this.property = data;
          this.images = data.imageUrls || []; // Utilizează `imageUrls` pentru imagini
        },
        error: (error) => {
          this.errorMessage = 'Error fetching property details. Please try again.';
          console.error('Fetch error:', error);
          this.router.navigate(['/properties']);
        },
      });
    } else {
      this.errorMessage = 'Property ID is missing!';
      this.router.navigate(['/properties']);
    }
  }

  onImageUpload(event: Event): void {
    const propertyId = this.property?.id;
    if (!propertyId) {
      alert('Property ID is missing!');
      return;
    }
  
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const formData = new FormData();
      formData.append('files', file); // Cheia corectă este 'files'
  
      this.uploading = true; // Setează indicatorul de upload
      this.propertyService.uploadImage(propertyId, formData).subscribe({
        next: (response) => {
          console.log('Upload successful:', response);
          if (response && response[0]?.url) {
            const imageUrl = response[0].url; // Extrage URL-ul imaginii din răspuns
            this.images.push(imageUrl); // Adaugă URL-ul în lista de imagini
          }
          this.uploading = false;
  
          // Resetează input-ul de fișiere
          input.value = ''; // Golește valoarea input-ului pentru a permite încărcări succesive
          alert('Image uploaded successfully!');
        },
        error: (error) => {
          this.uploading = false;
          console.error('Error uploading image:', error);
          alert('Failed to upload image. Please check the file and try again.');
        },
      });
    } else {
      alert('No file selected!');
    }
  }

  zoomedImage: string | null = null; // Variabilă pentru imaginea mărită

  openZoomModal(image: string): void {
    this.zoomedImage = image; // Setează imaginea care va fi afișată în modal
  }

  closeZoomModal(): void {
    this.zoomedImage = null; // Resetează imaginea pentru a închide modalul
  }
  



  isInquiryPopupOpen: boolean = false; // Control pentru deschiderea modalului
inquiryMessage: string = ''; // Mesajul trimis în Inquiry

openInquiryPopup(): void {
  this.isInquiryPopupOpen = true; // Deschide modalul
}

closeInquiryPopup(): void {
  this.isInquiryPopupOpen = false; // Închide modalul
  this.inquiryMessage = ''; // Resetează mesajul
}

sendInquiry(): void {
  if (!this.inquiryMessage.trim()) {
    alert('Please write a message before sending.');
    return;
  }

  const clientId = this.loginService.getUserId(); // Obține ID-ul utilizatorului
  if (!clientId) {
    alert('You need to be logged in to send an inquiry.');
    return;
  }

  if (this.property && this.property.id && this.property.userId) {
    const agentId = this.property.userId; // Proprietarul proprietății
    const status = 0; // Status implicit

    this.propertyService
      .sendInquiry(this.property.id, this.inquiryMessage, status, agentId, clientId)
      .subscribe({
        next: () => {
          alert('Your inquiry has been sent successfully!');
          this.closeInquiryPopup(); // Închide modalul
        },
        error: (err) => {
          console.error('Error sending inquiry:', err);
          alert('Failed to send inquiry. Please try again.');
        },
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