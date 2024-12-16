import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PropertyListComponent } from '../../components/property-list/property-list.component'

@Component({
  selector: 'app-properties-page',
  standalone: true,
  imports: [CommonModule, PropertyListComponent], // Import PropertyListComponent
  template: `
    <div class="properties-page">
      <h1>Property Management</h1>
      <app-property-list></app-property-list>
    </div>
  `,
  styleUrls: ['./properties-page.component.css']
})
export class PropertiesPageComponent { }
