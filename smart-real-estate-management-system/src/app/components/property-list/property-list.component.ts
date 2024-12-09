import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Property } from '../../models/property.model';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-property-list',
  imports: [NgFor],
  templateUrl: './property-list.component.html',
  styleUrl: './property-list.component.css'
})
export class PropertyListComponent implements OnInit {
Number(arg0: string): number {
throw new Error('Method not implemented.');
}

  properties: Property[] = [];
  pageNumber: number = 1;
  pageSize: number = 2;
  totalPages: number = 0;

  constructor(private propertyService: PropertyService, private router: Router) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.propertyService.getPropertiesWithPagination(this.pageNumber, this.pageSize)
      .subscribe((data: any) => {
        this.properties = data.items;
        this.totalPages = data.totalPages;
      });
  }

  navigateToCreate() {
    this.router.navigate(['properties/create']);
  }
  navigateToUpdate(property : Property) {
    this.router.navigate(['properties/update/' + property.id]);
  }
  viewDetails(property: Property) {
    this.router.navigate(['/properties/property-details', property.id]);
  }
  
  deleteProperty(property: Property): void {
    if (confirm('Are you sure you want to delete this property?')) {
        const propertyId = property.id;
        this.propertyService.deleteProperty(propertyId).subscribe({
            next: () => {
                this.properties = this.properties.filter(p => p !== property);

            },
            error: (err) => {
                console.error('Error deleting property:', err);
                alert('Failed to delete property.');
            }
        });
    }
}


  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.pageNumber = page;
      this.loadProperties();
    }
  }

  previousPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadProperties();
    }
  }

  nextPage(): void {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.loadProperties();
    }
  }
}
