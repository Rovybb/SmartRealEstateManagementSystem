import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../models/property.model';
import { LoginService } from './identity/login.service';

  @Injectable({
    providedIn: 'root'
  })
  export class PropertyService {

    //private apiURL = 'https://smartrealestatemanagementsystem-d7edeaecc4faccgx.polandcentral-01.azurewebsites.net/api/v1/Properties';
    private apiURL = 'https://localhost:7146/api/v1/Properties';
    constructor(private http: HttpClient) { }

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

    const token = this.getTokenFromCookies();
    const headers = { Authorization: `Bearer ${token}` };

    return this.http.get<any>(this.apiURL, { params, headers });
  }

  private getTokenFromCookies(): string {
    const name = 'token=';
    const decodedCookie = decodeURIComponent(document.cookie);
    const ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) === 0) {
        return c.substring(name.length, c.length);
      }
    }
    return '';
  }

  public createProperty(property: Property): Observable<any> {
    const token = this.getTokenFromCookies();
    const headers = { Authorization: `Bearer ${token}` };
    return this.http.post<Property>(this.apiURL, property, { headers });
  }

  public updateProperty(propertyId: string, property: Property): Observable<any> {
    const token = this.getTokenFromCookies();
    const headers = { Authorization: `Bearer ${token}` };
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.put<Property>(url, property, { headers });
  }

  public deleteProperty(propertyId: string): Observable<any> {
    const token = this.getTokenFromCookies();
    const headers = { Authorization: `Bearer ${token}` };
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.delete(url, { headers });
  }

  public getPropertyById(propertyId: string): Observable<Property> {
    const token = this.getTokenFromCookies();
    const headers = { Authorization: `Bearer ${token}` };
    const url = `${this.apiURL}/${propertyId}`;
    return this.http.get<Property>(url, { headers });
  }
}
