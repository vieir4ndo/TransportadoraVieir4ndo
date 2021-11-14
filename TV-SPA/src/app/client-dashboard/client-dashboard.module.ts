import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientDashboardRoutingModule } from './client-dashboard-routing.module';
import { ClientEditComponent } from './components/client-edit/client-edit.component';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ClientEditComponent
  ],
  imports: [
    CommonModule,
    ClientDashboardRoutingModule,
    FormsModule,
    SharedModule
  ],
  exports: [
    ClientEditComponent
  ]
})
export class ClientDashboardModule { }
