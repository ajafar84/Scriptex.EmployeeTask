import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeesIndexComponent } from './components/employees-index/employees-index.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';
import { EditEmployeeComponent } from './components/edit-employee/edit-employee.component';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [
    EmployeesIndexComponent,
    AddEmployeeComponent,
    EditEmployeeComponent
  ],
  imports: [CommonModule, EmployeeRoutingModule, SharedModule]
})
export class EmployeeModule {}
