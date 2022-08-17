import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeeDetailComponent } from './employee/employee-detail/employee-detail.component';
import { EmployeeEditComponent } from './employee/employee-edit/employee-edit.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path: 'employees', component: EmployeeComponent, children: [
      {path: '', component: EmployeeListComponent},
      {path: 'new', component: EmployeeEditComponent},
      {path: ':id', component: EmployeeDetailComponent},
      {path: ':id/edit', component: EmployeeEditComponent},
  ]},
  {path: '', redirectTo: '/employees', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
