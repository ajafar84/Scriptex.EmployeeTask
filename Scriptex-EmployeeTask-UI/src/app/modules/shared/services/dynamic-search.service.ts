import { Injectable } from '@angular/core';
import { FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { SearchModel } from '@shared/models/search-model.model';
import { TableColumn } from '@shared/interfaces/table-column.interface';
import { SearchOperator } from '@shared/models/search-operator.model';
import { SearchType } from '@shared/enums/search-type.enum';
import { ColumnType } from '@shared/enums/column-type.enum';

@Injectable({
  providedIn: 'root'
})
export class DynamicSearchService {
  searchOperators: SearchOperator[] = [
    {
      ArabicText: 'اكبر من',
      EnglishText: 'Greater Than',
      Operator: 'GreaterThan'
    },
    {
      ArabicText: 'اكبر من او يساوى',
      EnglishText: 'Greater Than Or Equal',
      Operator: 'GreaterThanOrEqual'
    },
    { ArabicText: 'اقل من', EnglishText: 'Less Than', Operator: 'LessThan' },
    {
      ArabicText: 'اقل من او يساوى',
      EnglishText: 'Less Than Or Equal',
      Operator: 'LessThanOrEqual'
    },
    { ArabicText: 'يحتوى', EnglishText: 'Contains', Operator: 'Contain' },
    {
      ArabicText: 'لا يحتوى',
      EnglishText: 'Not Contain',
      Operator: 'NotContain'
    },
    { ArabicText: 'يساوى', EnglishText: 'Equal', Operator: 'Equal' },
    { ArabicText: 'لا يساوى', EnglishText: 'Not Equal', Operator: 'NotEqual' }
  ];

  constructor(private formBuilder: FormBuilder) {}

  initializeSearch() {}

  lazy(event: any, searchModel: SearchModel, func: () => void) {
    searchModel.OrderBy = event.sortField;
    searchModel.OrderType = event.sortOrder === 1 ? 'asc' : 'desc';
    searchModel.PageNumber = event.first / event.rows + 1;
    searchModel.PageSize = event.rows;
    console.log('lazy');
    func();
  }

  buildSearchForm(cols: TableColumn[]) {
    const searchForm = this.formBuilder.group({
      SearchFields: this.formBuilder.array([])
    });
    const formArray = searchForm.get('SearchFields') as FormArray;
    cols.forEach(col => {
      if (!col.Hidden) {
        if (col.SearchType === SearchType.Select) {
          formArray.push(
            this.formBuilder.group({
              FieldName: [col.Field],
              Operator: ['Contain'],
              Value: ['']
            })
          );
        } else {
          formArray.push(
            this.formBuilder.group({
              FieldName: [col.Field],
              Operator: ['Contain'],
              Value: ['']
            })
          );
        }
      }
    });
    return searchForm;
  }

  reset(searchForm: FormGroup, func: () => void) {
    const formArray: FormArray = searchForm.get('SearchFields') as FormArray;

    for (const control of formArray.controls) {
      control.get('Value').reset();
    }

    func();
  }

  search(searchForm: FormGroup, searchModel: SearchModel, func: () => void) {
    searchModel.PageNumber = 1;
    searchModel.SearchFields = searchForm.get('SearchFields').value;
    func();
  }
}
