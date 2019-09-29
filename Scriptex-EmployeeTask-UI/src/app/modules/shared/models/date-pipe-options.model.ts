import { ColumnPipeOptions } from '@shared/interfaces/column-pipe-options.interface';
import { ColumnPipe } from '@shared/enums/column-pipe.enum';

export class DatePipeOptions implements ColumnPipeOptions {
  Format: string;
  TimeZone: string;
  Locale: string;
  PipeName: string;

  constructor(format?: string, timeZone?: string, locale?: string) {
    this.Format = format && format !== '' ? format : 'yyyy/MM/dd';
    this.TimeZone = timeZone;
    this.Locale = locale;
    this.PipeName = ColumnPipe.Date;
  }
}
