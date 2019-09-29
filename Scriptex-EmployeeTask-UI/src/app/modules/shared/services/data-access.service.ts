import { HttpClient } from '@angular/common/http';
import { ServiceResponseVM, ResponseVM } from '@shared/models/response.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SearchModel } from '@shared/models/search-model.model';
import { environment } from '@environments/environment';
import { HelperFunctions } from '@shared/helpers/helper-functions';

export class DataAccessService {
  constructor(
    public httpClient: HttpClient,
    private helperFunctions: HelperFunctions,
    public serviceUrl: string
  ) {}

  get(id: string | number): Observable<ServiceResponseVM> {
    return this.httpClient
      .get(`${this.serviceUrl}/Get/${id}`)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  getDetails(id: string | number): Observable<ServiceResponseVM> {
    return this.httpClient
      .get(`${this.serviceUrl}/GetDetails/${id}`)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  getListPage(searchModel: SearchModel): Observable<ServiceResponseVM> {
    return this.httpClient
      .post(`${this.serviceUrl}/GetListPage`, searchModel)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  getCreatePage(id?: string | number): Observable<ServiceResponseVM> {
    const serviceUrl = !id
      ? `${this.serviceUrl}/GetCreatePage`
      : `${this.serviceUrl}/GetCreatePage/${id}`;
    return this.httpClient
      .get(serviceUrl)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  getEditPage(id: string | number): Observable<ServiceResponseVM> {
    return this.httpClient
      .get(`${this.serviceUrl}/GetEditPage/${id}`)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  create(postedViewModel: any): Observable<ServiceResponseVM> {
    return this.httpClient
      .post(`${this.serviceUrl}/Create`, postedViewModel)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  save(postedViewModel: any): Observable<ServiceResponseVM> {
    return this.httpClient
      .post(`${this.serviceUrl}/Save`, postedViewModel)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  edit(postedViewModel: any): Observable<ServiceResponseVM> {
    return this.httpClient
      .put(`${this.serviceUrl}/Edit`, postedViewModel)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }

  delete(id: string | number) {
    return this.httpClient
      .delete(`${this.serviceUrl}/Delete/${id}`)
      .pipe(
        map((responseVM: ResponseVM) =>
          this.helperFunctions.handleResponse(responseVM)
        )
      );
  }
}
