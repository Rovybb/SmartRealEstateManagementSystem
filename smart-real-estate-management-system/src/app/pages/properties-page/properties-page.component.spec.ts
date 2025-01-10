import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertiesPageComponent } from './properties-page.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PropertiesPageComponent', () => {
  let component: PropertiesPageComponent;
  let fixture: ComponentFixture<PropertiesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        PropertiesPageComponent,  // Correctly importing the standalone component
        HttpClientTestingModule  // Fix: Adding HttpClientTestingModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PropertiesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render the page title', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Property Management');
  });

  it('should contain the app-property-list component', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('app-property-list')).toBeTruthy();
  });
});
