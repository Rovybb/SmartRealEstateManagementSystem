import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { PropertyListComponent } from './property-list.component';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';

describe('PropertyListComponent', () => {
  let component: PropertyListComponent;
  let fixture: ComponentFixture<PropertyListComponent>;
  let propertyServiceMock: any;
  let routerMock: any;

  beforeEach(async () => {
    // Cream un spy object pentru PropertyService cu metodele necesare
    propertyServiceMock = jasmine.createSpyObj('PropertyService', [
      'getPropertiesWithPagination',
      'getPropertyById'
    ]);

    // Cream un spy object pentru Router
    routerMock = jasmine.createSpyObj('Router', ['navigate']);

    // Configurăm răspunsul pentru getPropertiesWithPagination
    propertyServiceMock.getPropertiesWithPagination.and.returnValue(of({
      items: [{
        id: '8c868c11-e8db-4d11-a7c8-83ccb221305a',
        title: 'Modern Apartment',
        description: 'A beautiful apartment in the city center.',
        type: 0,
        status: 0,
        price: 100000,
        address: '123 Main St',
        area: 120,
        rooms: 3,
        bathrooms: 2,
        constructionYear: 2015,
        createdAt: new Date(),
        updatedAt: new Date(),
        userId: '3c868c18-e8db-4d11-a7c8-83ccb221305a'
      }],
      totalPages: 1
    }));

    // Configurăm răspunsul pentru getPropertyById (se presupune că se completează și array-ul de imagini)
    propertyServiceMock.getPropertyById.and.returnValue(of({
      id: '8c868c11-e8db-4d11-a7c8-83ccb221305a',
      title: 'Modern Apartment',
      description: 'A beautiful apartment in the city center.',
      type: 0,
      status: 0,
      price: 100000,
      address: '123 Main St',
      area: 120,
      rooms: 3,
      bathrooms: 2,
      constructionYear: 2015,
      createdAt: new Date(),
      updatedAt: new Date(),
      userId: '3c868c18-e8db-4d11-a7c8-83ccb221305a',
      imageUrls: ['img1.jpg', 'img2.jpg']
    }));

    await TestBed.configureTestingModule({
      // Deoarece componenta este standalone, o importăm direct
      imports: [ PropertyListComponent ],
      providers: [
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyListComponent);
    component = fixture.componentInstance;

    // Trigger initializarea componentei (ngOnInit)
    fixture.detectChanges();
  });

  it('should load properties and call getPropertyById for each property', () => {
    // Verificăm că lista de proprietăți a fost populată
    expect(component.properties.length).toBe(1);

    // Verificăm că apelul la getPropertiesWithPagination a fost făcut corect
    expect(propertyServiceMock.getPropertiesWithPagination)
      .toHaveBeenCalledWith(component.pageNumber, component.pageSize, {});

    // Verificăm apelul la getPropertyById pentru proprietatea returnată
    expect(propertyServiceMock.getPropertyById)
      .toHaveBeenCalledWith('8c868c11-e8db-4d11-a7c8-83ccb221305a');

    // Verificăm că proprietatea a primit array-ul de imagini
    expect(component.properties[0].imageUrls).toEqual(['img1.jpg', 'img2.jpg']);
  });

  it('should navigate to property details when viewDetails is called', () => {
    const property = component.properties[0];
    
    // Apelăm metoda viewDetails
    component.viewDetails(property);
    
    // Verificăm că router-ul este chemat cu ruta corectă
    expect(routerMock.navigate)
      .toHaveBeenCalledWith(['/properties/property-details', property.id]);
  });
});
