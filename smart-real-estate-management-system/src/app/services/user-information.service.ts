import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface UserInformation {
  email: string;
  username: string;
  firstName: string;
  lastName: string;
  address: string;
  phoneNumber: string;
  nationality: string;
  lastLogin?: Date;
  status: string;
  role: string;
  company?: string;
  type?: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserInformationService {
  private apiUrl = environment.API_URL + '/api/v1/UserInformation';
  constructor(private http: HttpClient) {}

  getUserById(id: string): Observable<UserInformation> {
    return this.http.get<UserInformation>(`${this.apiUrl}/${id}`);
  }
}
