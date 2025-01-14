import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserInformationService, UserInformation } from './user-information.service';
import { environment } from '../../environments/environment';

describe('UserInformationService', () => {
  let service: UserInformationService;
  let httpMock: HttpTestingController;

  const mockToken = 'mockToken123';
  const mockUser: UserInformation = {
    id: '123',
    email: 'test@example.com',
    username: 'testuser',
    firstName: 'Test',
    lastName: 'User',
    address: '123 Test St',
    phoneNumber: '1234567890',
    nationality: 'Testland',
    lastLogin: new Date(),
    status: 1,
    role: 2,
    company: 'Test Company',
    type: 'Admin',
  };

  beforeEach(() => {

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserInformationService],
    });

    service = TestBed.inject(UserInformationService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verificăm că nu există cereri nefinalizate
  });

  describe('getUserById', () => {
    it('should fetch user information by ID and include Authorization header', () => {
      service.getUserById(mockUser.id).subscribe((user) => {
        expect(user).toEqual(mockUser);
      });

      const req = httpMock.expectOne(`${environment.API_URL}/UserInformation/${mockUser.id}`);
      expect(req.request.method).toBe('GET');
      expect(req.request.headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      req.flush(mockUser); // Simulăm răspunsul API-ului
    });
  });

  describe('updateUser', () => {
    it('should send updated user data and include proper headers', () => {
      const updatedUser = { ...mockUser, firstName: 'Updated' };

      service.updateUser(mockUser.id, updatedUser).subscribe((response) => {
        expect(response).toBeTruthy();
      });

      const req = httpMock.expectOne(`${environment.API_URL}/UserInformation/${mockUser.id}`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.headers.get('Content-Type')).toBe('application/json');
      expect(req.request.headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(req.request.body).toEqual(updatedUser);

      req.flush({ success: true }); // Simulăm un răspuns de succes
    });
  });

  describe('getTokenFromCookies', () => {
    it('should correctly extract token from cookies', () => {
      const token = service['getTokenFromCookies'](); // Accesăm metoda privată
      expect(token).toBe(mockToken);
    });

    it('should return an empty string if token is not found', () => {
      const token = service['getTokenFromCookies']();
      expect(token).toBe('');
    });
  });
});
