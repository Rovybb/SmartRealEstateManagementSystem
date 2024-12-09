import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface RegisterPayload {
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = 'https://your-api-url.com/register'; // Înlocuiește cu URL-ul API-ului tău

  constructor(private http: HttpClient) { }

  // Metodă pentru a trimite datele de înregistrare la API
  register(email: string, password: string): Observable<any> {
    const payload: RegisterPayload = { email, password };
    return this.http.post(this.apiUrl, payload); // Trimite cererea POST
  }
}
