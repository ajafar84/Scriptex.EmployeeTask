import { JobVM } from '../job/job';

export interface EmployeeVM {
  Id: number;
  Name: string;
  JobId: number;
  Email: string;
  Mobile: string;
  NationalId: string;
  Gender: string;
  IsActive: boolean;
  CreationDate: Date | string;

  Job: JobVM;
}
