import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { provideHttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { appRoutes } from './app.routes';
import { PropertyService } from '../app/services/property.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ProfilePageComponent } from '../app/pages/profile-page/profile-page.component';

@NgModule({
    imports: [
        BrowserModule,
        CommonModule,
        FormsModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        RouterModule.forRoot(appRoutes),
        ProfilePageComponent,
    ],
    providers: [
        provideHttpClient(),
        PropertyService
    ],
})
export class AppModule {}
