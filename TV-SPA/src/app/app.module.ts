import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { AuthModule } from './auth/auth.module';
import { HomeComponent } from './pages/home/home.component';
import { ClientDashboardModule } from './client-dashboard/client-dashboard.module';

@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    AuthModule,
    ClientDashboardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
