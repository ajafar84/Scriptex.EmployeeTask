import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GlobalService } from '@shared/services/global.service';

@Injectable({
  providedIn: 'root'
})
export class HttpTokenInterceptor implements HttpInterceptor {
  currentLanguage = 'ar';

  constructor(private globalService: GlobalService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const headersConfig = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Language: this.currentLanguage
    };

    const request = req.clone({ setHeaders: headersConfig });
    return next
      .handle(request)
      .pipe(catchError(this.globalService.errorHandler));
  }
}
