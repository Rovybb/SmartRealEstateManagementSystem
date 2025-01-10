import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomePageComponent } from './home-page.component';
import { Router } from '@angular/router';
import { LoginService } from '../../services/identity/login.service';
import { of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

describe('HomePageComponent', () => {
  let component: HomePageComponent;
  let fixture: ComponentFixture<HomePageComponent>;
  let routerMock: any;
  let loginServiceMock: any;

  beforeEach(async () => {
    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    loginServiceMock = {
      isAuthenticated: jasmine.createSpy('isAuthenticated'),
      logout: jasmine.createSpy('logout')
    };

    await TestBed.configureTestingModule({
      imports: [HomePageComponent, CommonModule, MatSidenavModule, MatToolbarModule, MatButtonModule],
      providers: [
        { provide: Router, useValue: routerMock },
        { provide: LoginService, useValue: loginServiceMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the HomePageComponent', () => {
    expect(component).toBeTruthy();
  });

  it('should check if the user is logged in', () => {
    loginServiceMock.isAuthenticated.and.returnValue(of(true));

    component.isLogged().subscribe((isLogged) => {
      expect(isLogged).toBeTrue();
    });
    expect(loginServiceMock.isAuthenticated).toHaveBeenCalled();
  });

  it('should navigate to properties page', () => {
    component.navigateToProperties();
    expect(routerMock.navigate).toHaveBeenCalledWith(['properties']);
  });

  it('should navigate to register page', () => {
    component.navigateToRegister();
    expect(routerMock.navigate).toHaveBeenCalledWith(['auth/register']);
  });

  it('should navigate to login page', () => {
    component.navigateToLogin();
    expect(routerMock.navigate).toHaveBeenCalledWith(['auth/login']);
  });

  it('should log out the user', () => {
    component.logout();
    expect(loginServiceMock.logout).toHaveBeenCalled();
  });
});
