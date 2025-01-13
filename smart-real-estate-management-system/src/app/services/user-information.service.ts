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
  private apiUrl = environment.API_URL + '/UserInformation';
  constructor(private http: HttpClient) {}

  getUserById(id: string): Observable<UserInformation> {
    const token = this.getTokenFromCookies();
      // Setează header-ul cu token-ul JWT
    const headers = { Authorization: `Bearer ${token}` };
    return this.http.get<UserInformation>(`${this.apiUrl}/${id}`, { headers });
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
}
