import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProgressSpinnerService } from '@shared/services/progress-spinner.service';
import { Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProgressSpinnerInterceptor implements HttpInterceptor {
  constructor(private progressSpinnerService: ProgressSpinnerService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.progressSpinnerService.show();

    return next.handle(req).pipe(
      finalize(() => {
        this.progressSpinnerService.hide();
      })
    );
  }
}
