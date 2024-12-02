import { TestBed } from '@angular/core/testing';
import { PropertyService } from './property.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Property, PropertyStatus, PropertyType } from '../models/property.model';

describe('PropertyService', () => {
  let service: PropertyService;
  let httpTestingController: HttpTestingController;

  const apiURL = 'https://localhost:7146/api/v1/Properties';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PropertyService]
    });

    service = TestBed.inject(PropertyService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify(); // Ensure no outstanding requests
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all properties', () => {
    const mockProperties: Property[] = [
      {
        id: '1',
        title: 'Property 1',
        description: 'Description 1',
        type: PropertyType.HOUSE,
        status: PropertyStatus.AVAILABLE,
        price: 100000,
        address: 'Address 1',
        area: 120,
        rooms: 3,
        bathrooms: 2,
        constructionYear: 2005,
        createdAt: new Date(),
        updatedAt: new Date(),
        userId: 'user1'
      }
    ];

    service.getProperties().subscribe(properties => {
      expect(properties).toEqual(mockProperties);
    });

    const req = httpTestingController.expectOne(apiURL);
    expect(req.request.method).toBe('GET');
    req.flush(mockProperties); // Respond with mock data
  });

  it('should retrieve properties with pagination', () => {
    const mockResponse = {
      items: [
        {
          id: '1',
          title: 'Property 1',
          description: 'Description 1',
          type: 'HOUSE',
          status: 'AVAILABLE',
          price: 100000,
          address: 'Address 1',
          area: 120,
          rooms: 3,
          bathrooms: 2,
          constructionYear: 2005,
          createdAt: new Date(),
          updatedAt: new Date(),
          userId: 'user1'
        }
      ],
      totalPages: 1
    };

    service.getPropertiesWithPagination(1, 10).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpTestingController.expectOne(`${apiURL}?pageNumber=1&pageSize=10`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse); // Respond with mock data
  });

  it('should create a property', () => {
    const newProperty: Property = {
      id: '2',
      title: 'Property 2',
      description: 'Description 2',
      type: PropertyType.HOUSE,
      status: PropertyStatus.AVAILABLE,
      price: 200000,
      address: 'Address 2',
      area: 150,
      rooms: 4,
      bathrooms: 3,
      constructionYear: 2010,
      createdAt: new Date(),
      updatedAt: new Date(),
      userId: 'user2'
    };

    service.createProperty(newProperty).subscribe(response => {
      expect(response).toEqual(newProperty);
    });

    const req = httpTestingController.expectOne(apiURL);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newProperty);
    req.flush(newProperty); // Respond with mock data
  });

  it('should update a property', () => {
    const updatedProperty: Property = {
      id: '1',
      title: 'Updated Property',
      description: 'Updated Description',
      type: PropertyType.APARTMENT,
      status: PropertyStatus.SOLD,
      price: 150000,
      address: 'Updated Address',
      area: 130,
      rooms: 3,
      bathrooms: 2,
      constructionYear: 2008,
      createdAt: new Date(),
      updatedAt: new Date(),
      userId: 'user1'
    };

    service.updateProperty(1, updatedProperty).subscribe(response => {
      expect(response).toEqual(updatedProperty);
    });

    const req = httpTestingController.expectOne(`${apiURL}/1`);
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedProperty);
    req.flush(updatedProperty); // Respond with mock data
  });

  it('should delete a property', () => {
    service.deleteProperty(1).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpTestingController.expectOne(`${apiURL}/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null); // Respond with null for delete
  });
});
