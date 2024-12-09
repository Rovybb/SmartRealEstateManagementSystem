import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/identity/login.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private router: Router, private loginService: LoginService) { }

  // Returns an observable indicating if the user is logged in
  isLogged(): Observable<boolean> {
    return this.loginService.isAuthenticated();
  }

  navigateToProperties() {
    this.router.navigate(['properties']);
  }

  navigateToRegister() {
    this.router.navigate(['auth/register']);
  }

  navigateToLogin() {
    this.router.navigate(['auth/login']);
  }

  logout() {
    this.loginService.logout();
    this.router.navigate(['auth/login']); // Redirect to login page after logout
  }
}
