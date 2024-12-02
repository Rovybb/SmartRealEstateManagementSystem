import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  public getPropertiesWithPagination(
    pageNumber: number,
    pageSize: number,
    filters: { [key: string]: string } = {}
  ): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    for (const key in filters) {
      if (filters[key]) {
        params = params.set(key, filters[key]);
      }
    }

    return this.http.get<any>(this.apiURL, { params });
  }

  public createProperty(property: Property): Observable<any> {
    return this.http.post<Property>(this.apiURL, property);
  }

  public updateProperty(propertyId: string, property: Property): Observable<any> {
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.put<Property>(url, property);
  }

  public deleteProperty(propertyId: string): Observable<any> {
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.delete(url);
  }

  public getPropertyById(propertyId: string): Observable<Property> {
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.get<Property>(url);
  }
}
