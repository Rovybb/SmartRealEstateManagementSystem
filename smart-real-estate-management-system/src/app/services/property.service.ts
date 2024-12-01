import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../models/property.model';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  private apiURL = 'http://localhost:5219/api/v1/Properties';

  constructor(private http: HttpClient) { 
  }

  public getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(this.apiURL);
  }

  public createProperty(property: Property): Observable<any> {
    return this.http.post<Property>(this.apiURL, property);
  }
}
