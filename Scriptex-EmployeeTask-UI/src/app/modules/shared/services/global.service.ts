import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { MessageType } from '@shared/enums/message-type.enum';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  constructor(private toastr: ToastrService) {}

  //#region Messaging
  public messageAlert(messageType: MessageType, message: string) {
    switch (messageType) {
      case MessageType.Success:
        this.toastr.success(message);
        break;
      case MessageType.Info:
        this.toastr.info(message);
        break;
      case MessageType.Warning:
        this.toastr.warning(message);
        break;
      case MessageType.Error:
        this.toastr.error(message);
        break;
    }
  }

  //#endregion

  public errorHandler(error: HttpErrorResponse) {
    if (error.status === 401) {
      this.toastr.error('ليس لديك الصلاحية');
    } else {
      this.toastr.error('حدث خطأ ما من قبل الخادم (server)');
    }
    console.error(error);
    return throwError(error);
  }
}
