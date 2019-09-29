import { Component, OnInit } from '@angular/core';
import { TableColumn } from '@shared/interfaces/table-column.interface';
import { PagingMetaData } from '@shared/models/paging-meta-data.model';
import { FormGroup } from '@angular/forms';
import { SearchModel } from '@shared/models/search-model.model';
import { SearchOperator } from '@shared/models/search-operator.model';
import { Subscription } from 'rxjs';
import { DynamicSearchService } from '@shared/services/dynamic-search.service';
import { ColumnPipe } from '@shared/enums/column-pipe.enum';
import { ColumnType } from '@shared/enums/column-type.enum';
import { SearchType } from '@shared/enums/search-type.enum';
import { EmployeeService } from '../../services/employee.service';
import { EmployeePostVM } from '../../models/employee/employee-post';
import { HelperFunctions } from '@shared/helpers/helper-functions';

@Component({
  selector: 'app-employees-index',
  templateUrl: './employees-index.component.html',
  styleUrls: ['./employees-index.component.scss'],
  providers: [EmployeeService]
})
export class EmployeesIndexComponent implements OnInit {
  dataItems: any[];
  cols: TableColumn[];
  actions: TableAction[];

  serviceUrl: string;
  progressSpinner: boolean;

  // search form
  pagingMetaData: PagingMetaData;
  searchForm: FormGroup;
  searchModel: SearchModel;
  searchOperators: SearchOperator[];
  subscription: Subscription;
  checked: true;

  constructor(
    private dynamicSearchService: DynamicSearchService,
    private employeeService: EmployeeService,
    private helper: HelperFunctions
  ) {
    this.searchModel = {};
    this.progressSpinner = true;
    this.subscription = new Subscription();
    this.actions = [];
  }

  ngOnInit() {
    // build search form
    this.createCols();
    this.searchForm = this.dynamicSearchService.buildSearchForm(this.cols);
    this.searchOperators = this.dynamicSearchService.searchOperators;
    this.getData();
  }

  createCols() {
    this.cols = [
      { Field: '', Header: '', Type: ColumnType.CheckBox },
      { Field: 'Id', Header: 'الكود' },

      { Field: 'Name', Header: 'الاسم' },
      { Field: 'JobName', Header: 'الوظيفة' },
      { Field: 'Mobile', Header: 'الموبايل' },
      {
        Field: 'Email',
        Header: 'الإيميل'
      },
      {
        Field: 'CreationDate',
        Header: 'تاريخ الإضافة',
        Pipe: ColumnPipe.Date,
        PipeOptions: this.helper.getDefaultDatePipeOptions(),
        SearchType: SearchType.Date
      },
      {
        Field: 'IsActive',
        Header: 'الحالة',
        Searchable: false,
        Type: ColumnType.Status
      },
      {
        Field: 'Action',
        Header: 'الإجراءات',
        Searchable: false,
        Type: ColumnType.Action
      }
    ];
  }

  getData() {
    this.progressSpinner = true;

    this.subscription.add(
      this.employeeService.getListPage(this.searchModel).subscribe(
        result => {
          if (result.IsSuccess) {
            this.dataItems = result.Data.GridItemsVM;
            this.pagingMetaData = result.Data.PagingMetaData;
            console.log(this.dataItems);
          }
        },
        () => (this.progressSpinner = false),
        () => (this.progressSpinner = false)
      )
    );
  }

  onTableLazyLoad(event: any) {
    this.dynamicSearchService.lazy(event, this.searchModel, () =>
      this.getData()
    );
  }

  onSearchReset() {
    this.searchModel = {};
    this.dynamicSearchService.reset(this.searchForm, () => this.getData());
  }

  search() {
    this.dynamicSearchService.search(this.searchForm, this.searchModel, () =>
      this.getData()
    );
  }

  deleteEmployee(id: number) {
    if (confirm('هل تريد حذف هذا الموظف؟')) {
      this.employeeService.delete(id).subscribe(
        result => {
          if (result.IsSuccess) {
            this.getData();
          }
        },
        () => (this.progressSpinner = false),
        () => (this.progressSpinner = false)
      );
    }
  }

  changeStatus(id: number, e: any) {
    this.progressSpinner = true;
    const postedVM: EmployeePostVM = {
      Id: id,
      Email: null,
      Gender: null,
      IsActive: e.checked,
      JobId: null,
      Mobile: null,
      Name: null,
      NationalId: null
    };
    this.employeeService.changeStatus(postedVM).subscribe(
      result => {
        if (result.IsSuccess) {
          this.getData();
        }
      },
      () => (this.progressSpinner = false),
      () => (this.progressSpinner = false)
    );
  }
}
