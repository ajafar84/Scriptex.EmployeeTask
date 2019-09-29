import { ColumnType } from '@shared/enums/column-type.enum';
import { ColumnPipe } from '@shared/enums/column-pipe.enum';
import { ColumnPipeOptions } from '@shared/interfaces/column-pipe-options.interface';
import { SearchType } from '@shared/enums/search-type.enum';

export interface TableColumn {
  // Data Field Key
  Field: string;
  // Table Header Text
  Header: string;
  // Hide/Show Column
  Hidden?: boolean;
  // Allow Field to be Edited (Default Non Editable)
  Editable?: boolean;
  // Field Type (Default Data)
  Type?: ColumnType;
  // Apply Pipe to Data (Default No Pipe)
  Pipe?: ColumnPipe;
  // Pipe Options
  PipeOptions?: ColumnPipeOptions;
  // Search Options
  Searchable?: boolean;
  SearchType?: SearchType;
  // (Default Sortable)
  Sortable?: boolean;
}
