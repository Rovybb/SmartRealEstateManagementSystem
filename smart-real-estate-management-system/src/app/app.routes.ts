import { Routes } from '@angular/router';
import { PropertyListComponent } from './components/property-list/property-list.component';
import { PropertyCreateComponent } from './components/property-create/property-create.component';
import { PropertyUpdateComponent } from './components/property-update/property-update.component';
import { PropertyDetailComponent } from './components/property-detail/property-detail.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: '/properties', pathMatch: 'full' },
  { path: 'properties', component: PropertyListComponent },
  { path: 'properties/create', component: PropertyCreateComponent },
  { path: 'properties/update/:id', component: PropertyUpdateComponent },
  { path: 'properties/property-details/:id', component: PropertyDetailComponent }
];
