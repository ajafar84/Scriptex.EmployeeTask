// Server Side Result Returned for API Methods
export interface IResultVM {
  IsSuccess: boolean;
  FailedReason: string;
  AdditionalInfo: string;
  Data: any;
}

// Client Side Result Returned for Angular Service
export interface IServiceResult {
  isSuccess: boolean;
  data?: any;
}
