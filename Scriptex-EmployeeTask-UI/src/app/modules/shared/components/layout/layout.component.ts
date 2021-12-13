import { Component, OnInit } from '@angular/core';
import { ProgressSpinnerService } from '@shared/services/progress-spinner.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  showProgressSpinner: Observable<boolean> = this.progressSpinnerService
    .isLoading;

  constructor(private progressSpinnerService: ProgressSpinnerService) {}

  ngOnInit() {}
}
