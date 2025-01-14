import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Property } from '../../models/property.model';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarHomeComponent } from "../navbar-home/navbar-home.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faArrowRight, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-property-list',
  imports: [NgFor, FormsModule, NavbarHomeComponent, FontAwesomeModule],
  templateUrl: './property-list.component.html',
  styleUrl: './property-list.component.css'
})
export class PropertyListComponent implements OnInit {
  properties: Property[] = [];
  pageNumber: number = 1;
  pageSize: number = 2;
  totalPages: number = 0;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  faMagnifyingGlass = faMagnifyingGlass;

  filters: { [key: string]: string | number | null } = {
    title: null,
    price_min: null,
    price_max: null,
    description: null
  };

  constructor(private propertyService: PropertyService, private router: Router) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    const processedFilters: { [key: string]: string } = {};
  
    for (const key in this.filters) {
      const value = this.filters[key];
      if (value !== null && value !== undefined && value !== '') {
        processedFilters[key] = value.toString(); // Convertim valorile în string-uri
      }
    }
  
    console.log('Filters sent to backend:', processedFilters); // Debugging
  
    this.propertyService
      .getPropertiesWithPagination(this.pageNumber, this.pageSize, processedFilters)
      .subscribe((data: any) => {
        this.properties = data.items;
        this.totalPages = data.totalPages;
      });
  }

  navigateToCreate(): void {
    this.router.navigate(['properties/create']);
  }

  viewDetails(property: Property): void {
    this.router.navigate(['/properties/property-details', property.id]);
  }

  applyFilters(): void {
    console.log('Filters applied:', this.filters);
    this.pageNumber = 1;
    this.loadProperties();
  }
  

  resetFilters(): void {
    this.filters = {
      title: null,
      price_min: null,
      price_max: null,
      description: null
    };
    this.pageNumber = 1;
    this.loadProperties();
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