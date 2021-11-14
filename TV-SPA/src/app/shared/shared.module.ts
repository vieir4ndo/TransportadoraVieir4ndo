import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { RouterModule } from '@angular/router';
import { NgProgressModule } from '@ngx-progressbar/core';

import { BrowserModule } from '@angular/platform-browser';
import { AlertModule } from 'ngx-alerts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardComponent } from './layouts/dashboard/dashboard.component';
import { AuthComponent } from './layouts/auth/auth.component';

@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    DashboardComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgProgressModule,
    BrowserAnimationsModule,
    BrowserModule,
    BsDropdownModule.forRoot(),
    AlertModule.forRoot({ maxMessages: 5, timeout: 5000, positionX: 'right', positionY: 'top' })
  ],
  exports: [
    DashboardComponent,
    AuthComponent
  ]
})
export class SharedModule { }
