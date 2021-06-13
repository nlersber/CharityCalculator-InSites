import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { AdminComponent } from './components/admin/admin.component';
import { MaterialModule } from './components/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from './auth/auth.guard';
import { DonorComponent } from './components/donor/donor.component';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProviders } from './auth';
import { OptimalComponent } from './components/optimal/optimal.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    AdminComponent,
    DonorComponent,
    OptimalComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule, 
    FormsModule,
    HttpClientModule
  ],
  providers: [
    AuthGuard, 
    HttpClientModule, 
    httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
