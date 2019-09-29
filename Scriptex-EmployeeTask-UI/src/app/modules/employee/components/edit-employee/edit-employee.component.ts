import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeEditPageVM } from '../../models/employee/pages/employee-edit-page';
import { EmployeeService } from '../../services/employee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { GlobalService } from '@shared/services/global.service';
import { MessageType } from '@shared/enums/message-type.enum';
import { Regex } from '@shared/data/regex';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.scss']
})
export class EditEmployeeComponent implements OnInit {
  form: FormGroup;
  isFormSubmitted: boolean;
  employeeEditPageVM: EmployeeEditPageVM;
  employeeId: any;
  progressSpinner: boolean;
  gender: any;
  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      Id: ['', Validators.required],
      Name: ['', Validators.required],
      JobId: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Mobile: ['', [Validators.required, Validators.pattern(Regex.Mobile)]],
      Gender: ['', Validators.required],
      NationalId: [
        '',
        [Validators.required, Validators.pattern(Regex.NationalId)]
      ],
      IsActive: ['']
    });

    this.activatedRoute.queryParamMap.subscribe(params => {
      this.employeeId = params.get('Id');
      this.getEdit(this.employeeId);
    });
  }

  getEdit(employeeId: number) {
    this.progressSpinner = true;
    this.employeeService.getEditPage(employeeId).subscribe(
      response => {
        this.employeeEditPageVM = response.Data;

        this.form.patchValue({
          Id: this.employeeEditPageVM.EmployeeVM.Id,
          Name: this.employeeEditPageVM.EmployeeVM.Name,
          JobId: this.employeeEditPageVM.EmployeeVM.JobId,
          Email: this.employeeEditPageVM.EmployeeVM.Email,
          Mobile: this.employeeEditPageVM.EmployeeVM.Mobile,
          Gender: this.employeeEditPageVM.EmployeeVM.Gender,
          NationalId: this.employeeEditPageVM.EmployeeVM.NationalId,
          IsActive: this.employeeEditPageVM.EmployeeVM.IsActive
        });
        if (
          this.employeeEditPageVM.EmployeeVM.Gender.toLocaleLowerCase() === 'm'
        ) {
          this.gender = 'M';
        } else {
          this.gender = 'F';
        }
      },
      () => (this.progressSpinner = false),
      () => (this.progressSpinner = false)
    );
  }

  submit() {
    this.isFormSubmitted = true;
    this.progressSpinner = true;

    if (this.form.valid) {
      this.employeeService.edit(this.form.value).subscribe(
        response => {
          if (response.IsSuccess) {
            this.router.navigate(['/employee/index']);
          }
        },
        () => (this.progressSpinner = false),
        () => (this.progressSpinner = false)
      );
    }
  }
}
