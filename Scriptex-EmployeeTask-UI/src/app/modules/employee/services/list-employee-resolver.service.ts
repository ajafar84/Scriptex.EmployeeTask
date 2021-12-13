import { Injectable } from '@angular/core';
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { EmployeeVM } from '../models/employee/employee';
import { Observable } from 'rxjs';
import { EmployeeService } from './employee.service';
import { ServiceResponseVM } from '@shared/models/response.model';

@Injectable({
  providedIn: 'root',
})
export class ListEmployeeResolverService implements Resolve<ServiceResponseVM> {
  constructor(private employeeService: EmployeeService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<ServiceResponseVM> {
    return this.employeeService.getListPage({});
  }
}
