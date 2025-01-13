import { Routes } from '@angular/router';
import { PropertiesPageComponent } from './pages/properties-page/properties-page.component';
import { PropertyCreateComponent } from './components/property-create/property-create.component';
import { PropertyUpdateComponent } from './components/property-update/property-update.component';
import { LoginComponent } from './components/identity/login/login.component';
import { RegisterComponent } from './components/identity/register/register.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { PropertyDetailsPageComponent } from './pages/property-details-page/property-details-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { AuthGuard } from './auth.guard'; // Import the AuthGuard


export const appRoutes: Routes = [
  { path: '', component: HomePageComponent, pathMatch: 'full' },
  { path: 'home', redirectTo: '' },
  { path: 'properties', component: PropertiesPageComponent, canActivate: [AuthGuard] },
  { path: 'properties/create', component: PropertyCreateComponent, canActivate: [AuthGuard] },
  { path: 'properties/update/:id', component: PropertyUpdateComponent, canActivate: [AuthGuard] },
  { path: 'properties/property-details/:id', component: PropertyDetailsPageComponent, canActivate: [AuthGuard] },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/register', component: RegisterComponent },
  { path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard] }
];
