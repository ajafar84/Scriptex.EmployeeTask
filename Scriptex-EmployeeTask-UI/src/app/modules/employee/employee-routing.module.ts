import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeesIndexComponent } from './components/employees-index/employees-index.component';
import { EditEmployeeComponent } from './components/edit-employee/edit-employee.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';

const routes: Routes = [
  { path: 'index', component: EmployeesIndexComponent },
  { path: 'add', component: AddEmployeeComponent },
  { path: 'edit', component: EditEmployeeComponent },
  { path: '', redirectTo: 'index', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule {}
