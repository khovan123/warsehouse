type ErrorItem = {
  messagePosition: string;
  messageContent: string;
};

export type OtherErrorInfo = {
  validationError?: {
    pageErrorFocus?: string;
    errorType?: string;
    errorList?: ErrorItem[];
    isConfirmMode?: boolean;
  };
};

class ApiError extends Error {
  errorCode: string;
  errorMessage: string;
  errorDetail: string;
  otherErrorInfo?: OtherErrorInfo;
  status: number | undefined;
  constructor(
    errorCode: string,
    errorMessage: string,
    errorDetail: string,
    otherErrorInfo?: OtherErrorInfo,
    status?: number
  ) {
    super(errorMessage);
    this.errorCode = errorCode;
    this.errorMessage = errorMessage;
    this.errorDetail = errorDetail;
    this.otherErrorInfo = otherErrorInfo;
    this.status = status;
  }
}

export { ApiError };
