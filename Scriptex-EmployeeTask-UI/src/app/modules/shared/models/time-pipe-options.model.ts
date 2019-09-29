import { ColumnPipeOptions } from '@shared/interfaces/column-pipe-options.interface';
import { TimeFormat } from '@shared/enums/time-format.enum';
import { ColumnPipe } from '@shared/enums/column-pipe.enum';
import { CultureCode } from '@shared/enums/culture-code.enum';

export class TimePipeOptions implements ColumnPipeOptions {
  Format: TimeFormat;
  Localization: CultureCode;
  PipeName: string;

  constructor(format?: TimeFormat, localization?: CultureCode) {
    this.Format =
      format !== undefined && format !== null ? format : TimeFormat.TwelveHours;
    this.Localization =
      localization !== undefined && localization !== null
        ? localization
        : CultureCode.Ar;
    this.PipeName = ColumnPipe.Time;
  }
}
