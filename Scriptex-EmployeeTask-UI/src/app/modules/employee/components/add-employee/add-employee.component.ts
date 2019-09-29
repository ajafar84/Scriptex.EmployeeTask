import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeCreatePageVM } from '../../models/employee/pages/employee-create-page';
import { EmployeeService } from '../../services/employee.service';
import { Router } from '@angular/router';
import { Regex } from '@shared/data/regex';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit, OnDestroy {
  form: FormGroup;
  isFormSubmitted: boolean;
  employeeCreatePageVM: EmployeeCreatePageVM;
  progressSpinner: boolean;
  subscription: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router
  ) {
    this.subscription = new Subscription();
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      Name: ['', Validators.required],
      JobId: ['1', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Mobile: ['', [Validators.required, Validators.pattern(Regex.Mobile)]],
      Gender: ['', Validators.required],
      NationalId: [
        '',
        [Validators.required, Validators.pattern(Regex.NationalId)]
      ],
      IsActive: [true]
    });
    this.getCreate();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getCreate() {
    this.progressSpinner = true;
    this.subscription.add(
      this.employeeService.getCreatePage().subscribe(
        response => {
          if (response.IsSuccess) {
            this.employeeCreatePageVM = response.Data;
          }
        },
        () => (this.progressSpinner = false),
        () => (this.progressSpinner = false)
      )
    );
  }

  submit() {
    this.isFormSubmitted = true;
    this.progressSpinner = true;

    if (this.form.valid) {
      this.subscription.add(
        this.employeeService.create(this.form.value).subscribe(
          response => {
            if (response.IsSuccess) {
              this.router.navigate(['/employee/index']);
            }
          },
          () => (this.progressSpinner = false),
          () => (this.progressSpinner = false)
        )
      );
    }
  }
}
