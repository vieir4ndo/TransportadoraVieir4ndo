import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientEditComponent } from './components/client-edit/client-edit.component';

const routes: Routes = [
  { path: 'client-edit', component: ClientEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientDashboardRoutingModule { }
