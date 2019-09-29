import { Injectable } from '@angular/core';
import { HelperFunctions } from '@shared/helpers/helper-functions';
import { environment } from '@environments/environment';
import { DataAccessService } from '@shared/services/data-access.service';
import { HttpClient } from '@angular/common/http';
import { ResponseVM } from '@shared/models/response.model';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends DataAccessService {
  serviceUrl: string;

  constructor(
    private _httpClient: HttpClient,
    private _helper: HelperFunctions
  ) {
    super(_httpClient, _helper, `${environment.EmployeeTaskApiUrl}/employee/`);
    this.serviceUrl = `${environment.EmployeeTaskApiUrl}/employee/`;
  }

  changeStatus(postedModel: any) {
    return this._httpClient
      .put(`${this.serviceUrl}/ChangeStatus`, postedModel)
      .pipe(
        map((responseVM: ResponseVM) => this._helper.handleResponse(responseVM))
      );
  }
}
