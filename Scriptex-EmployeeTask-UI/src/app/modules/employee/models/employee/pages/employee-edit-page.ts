import { EmployeeVM } from '../employee';
import { JobVM } from '../../job/job';

export interface EmployeeEditPageVM {
  EmployeeVM: EmployeeVM;
  JobsVM: JobVM[];
}
