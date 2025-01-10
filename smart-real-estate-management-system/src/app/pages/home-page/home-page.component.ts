import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/identity/login.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatSidenavModule, MatToolbarModule, MatButtonModule],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent {
  constructor(private router: Router, private loginService: LoginService) {}

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
  }
}
