class ApiError extends Error {
  errorCode: string;
  errorMessage: string;
  errorDetail: string;
  errorPath: string;

  constructor(errorCode: string, errorMessage: string, errorDetail: string, errorPath: string) {
    super(errorMessage);
    this.errorCode = errorCode;
    this.errorMessage = errorMessage;
    this.errorDetail = errorDetail;
    this.errorPath = errorPath;
  }
}

type GraphqlError = {
  errors: {
    message: string;
    locations: {
      line: number;
      column: number;
    }[];
    path: string[];
    extensions: {
      code: string;
      stacktrace: string[];
    };
  }[];
  data?: unknown | null;
};

export { ApiError, type GraphqlError };
