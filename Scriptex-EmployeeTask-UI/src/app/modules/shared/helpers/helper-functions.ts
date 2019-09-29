import { Injectable } from '@angular/core';

import { DatePipeOptions } from '@shared/models/date-pipe-options.model';
import { CurrencyPipeOptions } from '@shared/models/currency-pipe-options.model';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  Validators,
  FormControl,
  AbstractControl
} from '@angular/forms';
import { FormValuesOptions } from '@shared/models/form-values-options.model';
import { GlobalService } from '@shared/services/global.service';
import { TimeFormat } from '@shared/enums/time-format.enum';
import { TimePipeOptions } from '@shared/models/time-pipe-options.model';
import { CultureCode } from '@shared/enums/culture-code.enum';
import { ResponseVM, ServiceResponseVM } from '@shared/models/response.model';
import { MessageType } from '@shared/enums/message-type.enum';
import { Regex } from '@shared/data/regex';

@Injectable({
  providedIn: 'root'
})
export class HelperFunctions {
  currentLanguage = 'ar';

  constructor(
    private formBuilder: FormBuilder,
    private globalService: GlobalService
  ) {}

  get Regex() {
    return Regex;
  }

  getYearRangeForDatePicker(isBirthDate = false) {
    const fromYear = new Date().getFullYear();
    const toYear = new Date().getFullYear() + 4;

    return isBirthDate ? `1900 : ${toYear}` : `${fromYear} : ${toYear}`;
  }

  getDateFormatForDatePicker() {
    let format: string;
    if (this.currentLanguage === CultureCode.Ar) {
      format = 'yy/mm/dd';
    } else if (this.currentLanguage === CultureCode.En) {
      format = 'dd/mm/yy';
    } else {
      return null;
    }

    return format;
  }

  getDefaultDatePipeOptions(isTime = false): DatePipeOptions {
    let format: string;

    if (this.currentLanguage === CultureCode.Ar) {
      format = isTime ? 'hh:mm:ss' : 'yyyy/MM/dd';
      return new DatePipeOptions(format);
    } else if (this.currentLanguage === CultureCode.En) {
      format = isTime ? 'hh:mm:ss' : 'dd/MM/yyyy';
      return new DatePipeOptions(format);
    } else {
      return null;
    }
  }

  getDefaultTimePipeOptions() {
    const format: TimeFormat = TimeFormat.TwelveHours;

    if (this.currentLanguage === CultureCode.Ar) {
      return new TimePipeOptions(format, CultureCode.Ar);
    } else if (this.currentLanguage === CultureCode.En) {
      return new TimePipeOptions(format, CultureCode.En);
    } else {
      return null;
    }
  }

  getDefaultCurrencyPipeOptions(): CurrencyPipeOptions {
    return new CurrencyPipeOptions('EGP', '', '1.2-2');
  }

  setFormValues(
    formGroup: FormGroup,
    viewModel: any,
    dropDownLists?: any,
    formValuesOptions?: FormValuesOptions
  ) {
    Object.keys(formGroup.controls).forEach(formControlkey => {
      let viewModelKey: string;
      // Get next form control
      const control: AbstractControl = formGroup.get(formControlkey);

      if (control instanceof FormGroup) {
        // Get view model key that corresponds to current formControlkey (form group key)
        viewModelKey = Object.keys(viewModel).find(k => k === formControlkey);

        if (viewModelKey === undefined) {
          // Try getting view model key that looks like (contains) formControlkey
          viewModelKey = Object.keys(viewModel).find(k =>
            this.isContainKeyWord(k, [formControlkey])
          );
          if (viewModelKey === undefined) {
            // view model doesn't have corresponding key to formControlkey
            return;
          }
        }
        // Recursivly call setFormValues method to parse the keys of the form group
        this.setFormValues(
          control,
          viewModel[viewModelKey],
          dropDownLists,
          formValuesOptions
        );
      } else if (control instanceof FormArray) {
        // To Do
      } else if (control instanceof FormControl) {
        // Get view model key that corresponds to current formControlkey (form control element key)
        viewModelKey = Object.keys(viewModel).find(k => k === formControlkey);

        if (viewModelKey === undefined) {
          // view model doesn't have corresponding key to formControlkey
          return;
        }

        // Check if the view model key represents Date value
        if (formValuesOptions.DateKeys.includes(viewModelKey.toLowerCase())) {
          viewModel[viewModelKey] = new Date(viewModel[viewModelKey]);
        } else if (
          // Check if the view model key represents Drop Down List Selected value
          formValuesOptions.DropDownKeys.includes(viewModelKey.toLowerCase())
        ) {
          // Get Drop Down List Key that corresponds to viewModelKey from dropDownLists
          const dropDownListKey = Object.keys(dropDownLists).find(k =>
            this.isContainKeyWord(k, [viewModelKey])
          );
          // Get Selected Drop Down Item Id Key that corresponds to viewModelKey from viewModel
          const selectedDropDownItemIdKey = Object.keys(viewModel).find(k =>
            this.isContainKeyWord(k, [`${viewModelKey}Id`])
          );
          // Get Current Selected Drop Down Item Object from dropDownLists
          viewModel[viewModelKey] = this.getSelectedItem(
            dropDownLists[dropDownListKey],
            viewModel[selectedDropDownItemIdKey]
          );
        }
        // Set Current Form Control Value from viewModel
        control.setValue(viewModel[viewModelKey]);
      }
    });
  }

  convertToFormArray(
    dataItems: any[],
    isRequired: boolean,
    isAllMembersRequired: boolean,
    ignoreKeyList: string[] = [],
    ignoreKeyWords: string[] = []
  ): FormArray {
    const formArray: FormArray = this.formBuilder.array(
      [],
      isRequired ? Validators.required : null
    );

    for (const dataItem of dataItems) {
      // Create a form group to hold the form controls corresponding to each dataItem element (key, value)
      const formGroup: FormGroup = this.formBuilder.group({});

      Object.keys(dataItem).forEach(key => {
        // Check if the current dataItem key is not included in both ignoreKeyList and ignoreKeyWords
        if (
          !ignoreKeyList.find(i => i.toLowerCase() === key.toLowerCase()) &&
          !this.isContainKeyWord(key, ignoreKeyWords)
        ) {
          // Add each dataItem element (key, value) to the current form group control
          formGroup.addControl(
            key,
            new FormControl(
              dataItem[key],
              isAllMembersRequired ? Validators.required : null
            )
          );
        }
      });

      formArray.push(formGroup);
    }

    return formArray;
  }

  clearFormArray(formArray: FormArray) {
    while (formArray.length !== 0) {
      formArray.removeAt(0);
    }
  }

  ResetFormArrayElements(formArray: FormArray, keys?: string[]) {
    for (const control of formArray.controls) {
      if (keys === undefined) {
        control.reset();
      } else {
        keys.forEach(key => {
          control.get(key).reset();
        });
      }
    }
  }

  getSelectedItem(viewModel: any, id: any) {
    if (id) {
      return viewModel.filter(
        (vm: any) => vm.Id.toString() === id.toString()
      )[0];
    }

    return null;
  }

  filterArray(event: any, arrayObject: any[], colName = 'FullName'): any[] {
    if (arrayObject !== undefined && arrayObject.length > 0) {
      const filteredArray = [];
      for (const item of arrayObject) {
        let itemValue = item[colName];

        itemValue = itemValue.replace(/\s/g, '').toLowerCase();
        const queryString = event.query.replace(/\s/g, '').toLowerCase();
        if (itemValue.indexOf(queryString) >= 0) {
          filteredArray.push(item);
        }
      }
      return filteredArray;
    } else {
      return null;
    }
  }

  handleResponse(responseVM: ResponseVM) {
    const serviceResponseVM: ServiceResponseVM = {
      IsSuccess: null,
      Data: null
    };
    if (responseVM.IsSuccess) {
      serviceResponseVM.Data = responseVM.Data;
      if (responseVM.Message) {
        this.globalService.messageAlert(
          MessageType.Success,
          responseVM.Message
        );
      }
    } else {
      serviceResponseVM.Data = responseVM.Data;
      this.globalService.messageAlert(MessageType.Error, responseVM.Message);
    }
    serviceResponseVM.IsSuccess = responseVM.IsSuccess;
    return serviceResponseVM;
  }

  private isContainKeyWord(key: string, keyWords: string[]): boolean {
    for (const keyWord of keyWords) {
      if (key.toLowerCase().search(keyWord.toLowerCase()) !== -1) {
        return true;
      }
    }

    return false;
  }
}
