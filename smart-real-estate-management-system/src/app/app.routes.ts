import { Routes } from '@angular/router';
import { PropertyListComponent } from './components/property-list/property-list.component';
import { PropertyCreateComponent } from './components/property-create/property-create.component';
import { PropertyUpdateComponent } from './components/property-update/property-update.component';
import { LoginComponent } from './components/identity/login/login.component';
import { RegisterComponent } from './components/identity/register/register.component';
import { HomeComponent } from './components/home/home.component';


export const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'properties', component: PropertyListComponent },
  { path: 'properties/create', component: PropertyCreateComponent },
  { path: 'properties/update/:id', component: PropertyUpdateComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/register', component: RegisterComponent }

];
