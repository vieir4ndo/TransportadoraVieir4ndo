import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColumnOneComponent } from './layouts/column-one/column-one.component';
import { HeaderComponent } from './components/header/header.component';
import { RouterModule } from '@angular/router';
import { NgProgressModule } from '@ngx-progressbar/core';

import { BrowserModule } from '@angular/platform-browser';
import { AlertModule } from 'ngx-alerts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    ColumnOneComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgProgressModule,
    BrowserAnimationsModule,
    BrowserModule,
    AlertModule.forRoot({ maxMessages: 5, timeout: 5000, positionX: 'right', positionY: 'top' })
  ],
  exports: [
    ColumnOneComponent,
    HeaderComponent
  ]
})
export class SharedModule { }
