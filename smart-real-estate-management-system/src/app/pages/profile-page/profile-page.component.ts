import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/identity/login.service';
import { UserInformationService, UserInformation } from '../../services/user-information.service';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { NavbarComponent } from "../../components/navbar/navbar.component";

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [
    CommonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    NavbarComponent
  ],
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponent implements OnInit {
  user: UserInformation | null = null;
  isAuthenticated: boolean = false;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(
    private router: Router,
    private loginService: LoginService,
    private userInformationService: UserInformationService
  ) {}

  ngOnInit(): void {
    // Subscribe to authentication status
    this.loginService.isAuthenticated$.subscribe(isAuth => {
      this.isAuthenticated = isAuth;
      if (isAuth) {
        this.fetchUserInformation();
      } else {
        this.user = null;
        this.isLoading = false;
      }
    });
  }

  // Fetch user information using the user ID from the token
  private fetchUserInformation(): void {
    const userId = this.loginService.getUserId();
    if (userId) {
      this.userInformationService.getUserById(userId).subscribe(
        (userInfo) => {
          this.user = userInfo;
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching user information:', error);
          this.error = 'Failed to load user information.';
          this.isLoading = false;
        }
      );
    } else {
      console.error('User ID not found in token.');
      this.error = 'Invalid authentication token.';
      this.isLoading = false;
    }
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
