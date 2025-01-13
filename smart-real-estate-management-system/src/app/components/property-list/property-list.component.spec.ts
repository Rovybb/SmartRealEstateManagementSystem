import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { PropertyListComponent } from './property-list.component';
import { PropertyService } from '../../services/property.service';
import { Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PropertyStatus, PropertyType } from '../../models/property.model';

/**
 * Dacă ai deja un model Property, importă-l direct.
 * Altfel, definiți local o variantă minimală pentru exemplu.
 */
export interface Property {
  id: string; // Dacă id-ul este generat automat, poate fi opțional
  title: string;
  description: string;
  status: PropertyStatus; // ENUM pentru status
  type: PropertyType; // ENUM pentru tipul proprietății
  price: number;
  address: string;
  area: number;
  rooms: number;
  bathrooms: number;
  constructionYear: number;
  userId: string; // Identificatorul utilizatorului
  createdAt: Date; // Data creării proprietății
  updatedAt: Date; // Data actualizării proprietății
}


describe('PropertyListComponent', () => {
  let component: PropertyListComponent;
  let fixture: ComponentFixture<PropertyListComponent>;
  let propertyServiceSpy: jasmine.SpyObj<PropertyService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    // Cream spy-uri pentru serviciile folosite de componentă.
    const propertyServiceMock = jasmine.createSpyObj('PropertyService', ['getPropertiesWithPagination']);
    const routerMock = jasmine.createSpyObj('Router', ['navigate']);

    // Configurăm metoda getPropertiesWithPagination să returneze un Observable valid, pentru a evita erorile la subscribe
    propertyServiceMock.getPropertiesWithPagination.and.returnValue(of({ items: [], totalPages: 0 }));

    await TestBed.configureTestingModule({
      // Deoarece PropertyListComponent este standalone, îl importăm în array-ul imports
      imports: [PropertyListComponent, HttpClientTestingModule],
      providers: [
        { provide: PropertyService, useValue: propertyServiceMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyListComponent);
    component = fixture.componentInstance;
    propertyServiceSpy = TestBed.inject(PropertyService) as jasmine.SpyObj<PropertyService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    fixture.detectChanges(); // Inițializează componenta și declanșează ngOnInit
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  describe('loadProperties', () => {
    it('should call getPropertiesWithPagination with processed filters and update component data', () => {
      // Simulăm un răspuns de la serviciu cu datele așteptate
      const mockResponse = {
        items: [
          { id: 1, title: 'Test Property', price: 100 }
        ],
        totalPages: 3
      };

      propertyServiceSpy.getPropertiesWithPagination.and.returnValue(of(mockResponse));

      // Setăm câteva filtre; doar cele nenule și diferite de string gol vor fi procesate
      component.filters = {
        title: 'House',
        price_min: null,
        price_max: '',
        description: 'Nice property'
      };

      // Apelăm metoda loadProperties
      component.loadProperties();

      // Verificăm apelul serviciului cu parametrii corecți
      expect(propertyServiceSpy.getPropertiesWithPagination).toHaveBeenCalledWith(
        component.pageNumber,  // de regulă 1 inițial
        component.pageSize,    // exemplu
        { title: 'House', description: 'Nice property' }
      );

      // Verificăm actualizarea datelor în componentă
      expect(component.properties.length).toBe(1);
      expect(component.totalPages).toBe(3);
    });
  });

  describe('navigation methods', () => {
    it('should navigate to create property page when navigateToCreate is called', () => {
      component.navigateToCreate();
      expect(routerSpy.navigate).toHaveBeenCalledWith(['properties/create']);
    });

    it('should navigate to property details when viewDetails is called', () => {
      const testProperty: Property = {
        id: '6fa459ea-ee8a-3ca4-894e-db77e160355e', // `id` este de tip string conform interfeței
        title: 'Test',
        description: 'Test description',
        status: PropertyStatus.AVAILABLE, // Sau orice altă valoare validă din enum
        type: PropertyType.APARTMENT, // Sau orice altă valoare validă din enum
        price: 999,
        address: '123 Test St',
        area: 100,
        rooms: 3,
        bathrooms: 2,
        createdAt: new Date(),
        updatedAt: new Date(),
        constructionYear: 2020,
        userId: '6fa459ea-ee8a-3ca4-894e-db77e160355e'
      };
      
      // Apelăm metoda viewDetails cu un obiect de test
      component.viewDetails(testProperty);
      expect(routerSpy.navigate).toHaveBeenCalledWith(['/properties/property-details', 10]);
    });
  });

  describe('filters', () => {
    beforeEach(() => {
      // Spionăm metoda loadProperties pentru a verifica că este apelată
      spyOn(component, 'loadProperties');
    });

    it('applyFilters should set pageNumber to 1 and call loadProperties', () => {
      component.pageNumber = 5;
      component.applyFilters();
      expect(component.pageNumber).toBe(1);
      expect(component.loadProperties).toHaveBeenCalled();
    });

    it('resetFilters should clear filters, set pageNumber to 1, and call loadProperties', () => {
      component.filters = {
        title: 'Something',
        price_min: 50,
        price_max: 100,
        description: 'Test desc'
      };
      component.pageNumber = 3;

      component.resetFilters();

      expect(component.filters).toEqual({
        title: null,
        price_min: null,
        price_max: null,
        description: null
      });
      expect(component.pageNumber).toBe(1);
      expect(component.loadProperties).toHaveBeenCalled();
    });
  });

  describe('pagination methods', () => {
    beforeEach(() => {
      spyOn(component, 'loadProperties');
      component.totalPages = 5; // Setăm un total de pagini pentru teste
    });

    it('goToPage should change pageNumber and call loadProperties if page is valid', () => {
      component.goToPage(3);
      expect(component.pageNumber).toBe(3);
      expect(component.loadProperties).toHaveBeenCalled();
    });

    it('goToPage should not call loadProperties if page is outside the range', () => {
      component.pageNumber = 2;
      component.goToPage(10); // 10 > totalPages
      expect(component.pageNumber).toBe(2);
      expect(component.loadProperties).not.toHaveBeenCalled();
    });

    it('previousPage should decrement pageNumber and call loadProperties if pageNumber > 1', () => {
      component.pageNumber = 3;
      component.previousPage();
      expect(component.pageNumber).toBe(2);
      expect(component.loadProperties).toHaveBeenCalled();
    });

    it('previousPage should not decrement pageNumber if pageNumber is already 1', () => {
      component.pageNumber = 1;
      component.previousPage();
      expect(component.pageNumber).toBe(1);
      expect(component.loadProperties).not.toHaveBeenCalled();
    });

    it('nextPage should increment pageNumber and call loadProperties if pageNumber < totalPages', () => {
      component.pageNumber = 2;
      component.nextPage();
      expect(component.pageNumber).toBe(3);
      expect(component.loadProperties).toHaveBeenCalled();
    });

    it('nextPage should not increment pageNumber if pageNumber equals totalPages', () => {
      component.pageNumber = 5;
      component.nextPage();
      expect(component.pageNumber).toBe(5);
      expect(component.loadProperties).not.toHaveBeenCalled();
    });
  });
});
