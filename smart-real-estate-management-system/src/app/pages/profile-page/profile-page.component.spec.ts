import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProfilePageComponent } from './profile-page.component';
import { Router } from '@angular/router';
import { of, throwError, Subject } from 'rxjs';
import { LoginService } from '../../services/identity/login.service';
import { UserInformationService } from '../../services/user-information.service';
import { MatDialog } from '@angular/material/dialog';
import { RouterTestingModule } from '@angular/router/testing';

// Mock data
const mockUser = {
  id: '123fweewf23-23ewdfwef-23ewfwe',
  email: 'test.user@example.com',
  username: 'testuser',
  firstName: 'Test',
  lastName: 'User',
  role: 1,
  status: 1,
  address: '123 Test St',
  phoneNumber: '1234567890',
  nationality: 'Testland',
  lastLogin: new Date(),
  
};

describe('ProfilePageComponent', () => {
  let component: ProfilePageComponent;
  let fixture: ComponentFixture<ProfilePageComponent>;
  let loginServiceMock: any;
  let userInformationServiceMock: any;
  let dialogMock: any;
  let router: Router;
  let authStatusSubject: Subject<boolean>;

  beforeEach(async () => {
    // Create spies for services
    authStatusSubject = new Subject<boolean>();

    loginServiceMock = jasmine.createSpyObj('LoginService', ['isAuthenticated$', 'getUserId', 'logout']);
    loginServiceMock.isAuthenticated$ = authStatusSubject.asObservable();
    loginServiceMock.getUserId.and.returnValue('123');
    loginServiceMock.logout.and.returnValue(of(true));

    userInformationServiceMock = jasmine.createSpyObj('UserInformationService', ['getUserById']);
    userInformationServiceMock.getUserById.and.returnValue(of(mockUser));

    dialogMock = jasmine.createSpyObj('MatDialog', ['open']);

    await TestBed.configureTestingModule({
      imports: [ProfilePageComponent, RouterTestingModule],
      providers: [
        { provide: LoginService, useValue: loginServiceMock },
        { provide: UserInformationService, useValue: userInformationServiceMock },
        { provide: MatDialog, useValue: dialogMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfilePageComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should return correct user role', () => {
    expect(component.getUserRole(0)).toBe('client');
    expect(component.getUserRole(1)).toBe('proffesional');
    expect(component.getUserRole(999)).toBe('unknown');
  });

  it('should return correct user status', () => {
    expect(component.getUserStatus(0)).toBe('active');
    expect(component.getUserStatus(1)).toBe('inactive');
    expect(component.getUserStatus(2)).toBe('banned');
    expect(component.getUserStatus(999)).toBe('unknown');
  });

  it('should navigate to login', () => {
    spyOn(router, 'navigate');
    component.navigateToLogin();
    expect(router.navigate).toHaveBeenCalledWith(['auth/login']);
  });

  it('should navigate to register', () => {
    spyOn(router, 'navigate');
    component.navigateToRegister();
    expect(router.navigate).toHaveBeenCalledWith(['auth/register']);
  });

  it('should navigate to properties', () => {
    spyOn(router, 'navigate');
    component.navigateToProperties();
    expect(router.navigate).toHaveBeenCalledWith(['properties']);
  });

  it('should logout', () => {
    component.logout();
    expect(loginServiceMock.logout).toHaveBeenCalled();
  });

  it('should fetch user information on authentication', () => {
    authStatusSubject.next(true);
    expect(loginServiceMock.getUserId).toHaveBeenCalled();
    expect(userInformationServiceMock.getUserById).toHaveBeenCalledWith('123');
    expect(component.user).toEqual(mockUser);
    expect(component.isLoading).toBeFalse();
  });

  it('should handle error if fetching user information fails', () => {
    userInformationServiceMock.getUserById.and.returnValue(throwError(() => new Error('Error fetching user info')));
    component.fetchUserInformation();
    expect(component.error).toBe('Failed to load user information.');
    expect(component.isLoading).toBeFalse();
  });

  it('should open update dialog and refresh user data on close', () => {
    dialogMock.open.and.returnValue({
      afterClosed: () => of(true),
    });
    spyOn(component, 'fetchUserInformation');
    component.openUpdateDialog();
    expect(dialogMock.open).toHaveBeenCalled();
    expect(component.fetchUserInformation).toHaveBeenCalled();
  });

  it('should not open update dialog if user is null', () => {
    component.user = null;
    component.openUpdateDialog();
    expect(dialogMock.open).not.toHaveBeenCalled();
  });

  it('should update isAuthenticated on authentication status change', () => {
    authStatusSubject.next(true);
    expect(component.isAuthenticated).toBeTrue();

    authStatusSubject.next(false);
    expect(component.isAuthenticated).toBeFalse();
  });
});