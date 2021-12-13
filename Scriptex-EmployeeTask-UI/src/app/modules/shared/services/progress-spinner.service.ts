import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProgressSpinnerService {
  // Observable Source
  private isLoadingSource = new Subject<boolean>();

  // Observable Stream
  isLoading = this.isLoadingSource.asObservable();

  constructor() {}

  show() {
    this.isLoadingSource.next(true);
  }

  hide() {
    this.isLoadingSource.next(false);
  }
}
