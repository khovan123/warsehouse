class ApiError extends Error {
  status: number;
  title: string;
  detail?: string;
  instance?: string;
  traceId?: string;
  errors?: Record<string, unknown>;

  constructor(
    status: number,
    title: string,
    detail: string,
    instance: string,
    traceId: string,
    errors: Record<string, unknown>
  ) {
    super(title);
    this.status = status;
    this.title = title;
    this.detail = detail;
    this.instance = instance;
    this.traceId = traceId;
    this.errors = errors;
  }
}

// type GraphqlError = {
//   errors: {
//     message: string;
//     locations: {
//       line: number;
//       column: number;
//     }[];
//     path: string[];
//     extensions: {
//       code: string;
//       stacktrace: string[];
//     };
//   }[];
//   data?: unknown | null;
// };

export { ApiError };
