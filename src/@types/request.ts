import type {
  AxiosInstance,
  Method as AxiosMethod,
  AxiosPromise,
  AxiosRequestConfig,
  AxiosResponse,
} from 'axios';

export type Method = AxiosMethod;
export type RequestPromise<T> = AxiosPromise<T>;
export type HttpClientInstance = AxiosInstance;
export type HttpResponse<T> = AxiosResponse<T>;
export type RequestConfig = AxiosRequestConfig;
