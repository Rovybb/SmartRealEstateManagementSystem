import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Router } from '@angular/router';  // Importă Router

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = 'https://localhost:7146/api/Auth/login';  // URL-ul pentru login
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) { }  // Injectează Router

  // Metodă pentru logarea utilizatorului
  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };

    return this.http.post<any>(this.apiUrl, loginData).pipe(
      catchError((error) => {
        console.error('Login failed', error);
        throw error;
      }),
      tap((response) => {
        if (response && response.token) {
          document.cookie = `token=${response.token}; path=/;`;
          this.setAuthenticationState(true);
          this.router.navigate(['/']);  // Redirecționează utilizatorul pe Home
        }
      })
    );
  }

  // Setează starea autentificării
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
    document.cookie = 'token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    console.log('User logged out');
    this.router.navigate(['/']); // Redirecționează utilizatorul pe Home după logout
  }
}
