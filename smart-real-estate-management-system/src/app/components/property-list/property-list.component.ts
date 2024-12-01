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
        this.properties = data.items; // Assume the API returns `items` and `totalPages`
        this.totalPages = data.totalPages;
      });
  }

  navigateToCreate() {
    this.router.navigate(['properties/create']);
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
