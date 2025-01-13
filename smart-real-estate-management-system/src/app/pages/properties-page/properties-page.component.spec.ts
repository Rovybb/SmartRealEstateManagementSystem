import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PropertiesPageComponent } from './properties-page.component';
import { By } from '@angular/platform-browser';
import { PropertyListComponent } from '../../components/property-list/property-list.component';

describe('PropertiesPageComponent', () => {
  let component: PropertiesPageComponent;
  let fixture: ComponentFixture<PropertiesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // Deoarece PropertiesPageComponent este standalone și importă PropertyListComponent, le importăm pe ambele
      imports: [PropertiesPageComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertiesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // Declanșează ciclul de viață al componentei
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should render heading "Property Management"', () => {
    const heading = fixture.debugElement.query(By.css('h1')).nativeElement;
    expect(heading.textContent).toContain('Property Management');
  });

  it('should include the property list component', () => {
    const propertyListDebugEl = fixture.debugElement.query(By.directive(PropertyListComponent));
    expect(propertyListDebugEl).toBeTruthy();
  });
});
