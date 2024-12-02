  import { Injectable } from '@angular/core';
  import { HttpClient } from '@angular/common/http';
  import { Observable } from 'rxjs';
  import { Property } from '../models/property.model';

  @Injectable({
    providedIn: 'root'
  })
  export class PropertyService {

    private apiURL = 'https://smartrealestatemanagementsystem-d7edeaecc4faccgx.polandcentral-01.azurewebsites.net/api/v1/Properties';

    constructor(private http: HttpClient) { 
    }

    public getProperties(): Observable<Property[]> {
      return this.http.get<Property[]>(this.apiURL);
    }

    public getPropertiesWithPagination(pageNumber: number, pageSize: number): Observable<any> {
      return this.http.get<any>(`${this.apiURL}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
    } 

  public createProperty(property: Property): Observable<any> {
    return this.http.post<Property>(this.apiURL, property);
  }
  public updateProperty(propertyId: number, property: Property): Observable<any> {
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.put<Property>(url, property);
  }
  public deleteProperty(propertyId: number): Observable<any> {
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.delete(url);
  }
}
