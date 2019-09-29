import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './modules/shared/components/layout/layout.component';

const routes: Routes = [
  {
    path: 'employee',
    children: [
      {
        path: '',
        loadChildren: './modules/employee/employee.module#EmployeeModule'
      },
      { path: '', redirectTo: 'employee', pathMatch: 'full' }
    ],
    component: LayoutComponent
  },
  { path: '', redirectTo: 'employee', pathMatch: 'full' },
  { path: '**', redirectTo: 'employee', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
