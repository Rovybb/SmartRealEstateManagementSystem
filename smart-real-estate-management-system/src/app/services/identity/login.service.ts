import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'https://example.com/api/login';  // URL-ul API-ului pentru login
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) { }

  // Metodă pentru logarea utilizatorului
  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };

    return this.http.post<any>(this.apiUrl, loginData).pipe(
      catchError((error) => {
        console.error('Login failed', error);
        throw error;
      })
    );
  }

  // Setează starea autentificării (dacă utilizatorul este autentificat sau nu)
  setAuthenticationState(isAuthenticated: boolean): void {
    this.isAuthenticatedSubject.next(isAuthenticated);
  }

  // Verifică dacă utilizatorul este autentificat
  isAuthenticated(): Observable<boolean> {
    return this.isAuthenticated$;
  }

  // Metodă pentru logout
  logout(): void {
    this.setAuthenticationState(false);
    // Eventual, aici poți să ștergi tokenul de autentificare sau alte date de sesiune
    console.log('User logged out');
  }
}
