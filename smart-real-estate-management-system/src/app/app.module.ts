import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { provideHttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from "@angular/platform-browser/animations";

import { appRoutes } from './app.routes';
import { PropertyService } from '../app/services/property.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


@NgModule({
    declarations: [
    ],
    imports: [
      BrowserModule,
      CommonModule,
      BrowserAnimationsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes),
    ],
    providers: [provideHttpClient(), PropertyService],
  })
  export class AppModule {}
  