import { Pipe, PipeTransform } from '@angular/core';
import { TableColumn } from '@shared/interfaces/table-column.interface';

@Pipe({
  name: 'hiddenColumnFilter'
})
export class HiddenColumnFilterPipe implements PipeTransform {
  transform(cols: TableColumn[]): TableColumn[] {
    return cols.filter(col => !col.Hidden);
  }
}
