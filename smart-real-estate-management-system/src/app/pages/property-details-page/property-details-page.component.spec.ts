import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertyDetailsPageComponent } from './property-details-page.component';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';

describe('PropertyDetailsPageComponent', () => {
  let component: PropertyDetailsPageComponent;
  let fixture: ComponentFixture<PropertyDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        PropertyDetailsPageComponent,  // Correct standalone import
        HttpClientTestingModule        // Provides HttpClient mock
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: jasmine.createSpy('get').and.returnValue('1')  // Correctly mocked
              }
            }
          }
        },
        {
          provide: Router,
          useValue: {
            navigate: jasmine.createSpy('navigate')
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PropertyDetailsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should contain the app-property-detail component', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('app-property-detail')).toBeTruthy();
  });
});
