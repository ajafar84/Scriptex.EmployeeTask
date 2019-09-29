import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './global.service';
import { Observable, pipe } from 'rxjs';
import { ServiceResponseVM, ResponseVM } from '@shared/models/response.model';
import { SearchModel } from '@shared/models/search-model.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TableService {
  constructor(private httpClient: HttpClient) {}

  getListPage(
    serviceUrl: string,
    searchModel: SearchModel
  ): Observable<ServiceResponseVM> {
    const serviceResponseVM: ServiceResponseVM = {
      IsSuccess: null,
      Data: null
    };
    return this.httpClient.post(`${serviceUrl}/GetListPage`, searchModel).pipe(
      map((responseVM: ResponseVM) => {
        if (responseVM.IsSuccess) {
          serviceResponseVM.Data = responseVM.Data;
        } else {
        }
        serviceResponseVM.IsSuccess = responseVM.IsSuccess;
        return serviceResponseVM;
      })
    );
  }

  getListPageWithId(
    id: string,
    serviceUrl: string,
    searchModel: SearchModel
  ): Observable<ServiceResponseVM> {
    const serviceResponseVM: ServiceResponseVM = {
      IsSuccess: null,
      Data: null
    };
    return this.httpClient.post(`${serviceUrl}/${id}`, searchModel).pipe(
      map((responseVM: ResponseVM) => {
        if (responseVM.IsSuccess) {
          serviceResponseVM.Data = responseVM.Data;
        } else {
        }
        serviceResponseVM.IsSuccess = responseVM.IsSuccess;
        return serviceResponseVM;
      })
    );
  }
}
