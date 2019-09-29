import { ColumnPipeOptions } from '@shared/interfaces/column-pipe-options.interface';
import { ColumnPipe } from '@shared/enums/column-pipe.enum';

export class CurrencyPipeOptions implements ColumnPipeOptions {
  CurrencyCode: string;
  Display: string;
  DigitsInfo: string;
  Locale: string;
  PipeName: string;

  constructor(
    currencyCode?: string,
    display?: string,
    digitsInfo?: string,
    locale?: string
  ) {
    this.CurrencyCode = currencyCode;
    this.Display = display && display !== '' ? display : 'symbol';
    this.DigitsInfo = digitsInfo;
    this.Locale = locale;
    this.PipeName = ColumnPipe.Currency;
  }
}
