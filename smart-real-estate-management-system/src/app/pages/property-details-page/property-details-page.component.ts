import { Component } from '@angular/core';
import { PropertyDetailComponent } from '../../components/property-detail/property-detail.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-details-page',
  standalone: true,
  imports: [CommonModule, PropertyDetailComponent], // Import PropertyDetailComponent
  templateUrl: './property-details-page.component.html',
  styleUrls: ['./property-details-page.component.css']
})
export class PropertyDetailsPageComponent { }
