import axios, { AxiosError } from 'axios';

import type { HttpClientInstance, RequestConfig } from '@/@types/request';

import { ApiError, type GraphqlError } from './type';

class HttpClient {
  private config: RequestConfig;
  private client: HttpClientInstance | null = null;
  // private contextHash: string | null = null;

  constructor(config: RequestConfig) {
    this.config = config;
  }
  // private updateContextHash(newHash: string): void {
  //   this.contextHash = newHash;
  //   document.cookie = `x-sharex-context-hash=${newHash}; path=/; secure;`;
  // }

  // private getCookieValue(_key: string): string {
  //   return '';
  // }

  // private restoreContextHash(): void {
  //   this.contextHash = this.getCookieValue('x-sharex-context-hash');
  // }

  // private handleExpired(): void {
  //   console.warn('Session expired. Redirect to login...');
  // }

  private buildApiError(error: unknown): ApiError {
    let errorCode;
    let errorMessage;
    let errorDetail;
    let errorPath;

    if ((error as GraphqlError).errors) {
      const refinedData = error as Record<string, unknown>;
      if (Array.isArray(refinedData.errors) && refinedData.errors.length > 0) {
        const gqlError = refinedData.errors[0];
        const extensions = gqlError.extensions || {};

        errorMessage = gqlError.message;

        errorCode = extensions.code;

        errorDetail =
          extensions.stacktrace ||
          (Array.isArray(extensions.stacktrace)
            ? extensions.stacktrace.join(',')
            : extensions.stacktrace) ||
          gqlError.message;

        errorPath = gqlError.path;

        return new ApiError(
          errorCode as string,
          errorMessage as string,
          errorDetail as string,
          errorPath as string
        );
      }
    }
    if (error as AxiosError) {
      const { code, config, message, stack } = error as AxiosError;

      errorCode = code;
      errorMessage = message;
      errorDetail = stack;
      errorPath = [config?.baseURL, config?.url].join('');

      return new ApiError(
        (errorCode as string) || 'UNKNOWN_ERROR',
        (errorMessage as string) || 'UNKNOWN_ERROR',
        (errorDetail as string) || 'UNKNOWN_ERROR',
        (errorPath as string) || 'UNKNOW_ERROR_PATH'
      );
    }

    return new ApiError(
      'UNKNOWN_ERROR_CODE',
      'UNKNOWN_ERROR_MESSAGE',
      'UNKNOWN_ERROR_DETAIL',
      'UNKNOW_ERROR_PATH'
    );
  }

  private processApiError = (error: unknown): Promise<unknown> => {
    let apiError = null;
    if ((error as GraphqlError).errors || (error as AxiosError)) {
      apiError = this.buildApiError(error);
    } else {
      apiError = new ApiError(
        'UNKNOWN_ERROR_CODE',
        'UNKNOWN_ERROR_MESSAGE',
        'UNKNOWN_ERROR_DETAIL',
        'UNKNOW_ERROR_PATH'
      );
    }
    if (!apiError) {
      apiError = error;
    }

    return Promise.reject(apiError);
  };

  get<T>(uri: string, option?: RequestConfig): Promise<T> {
    return this.getClient().get(uri, option);
  }

  post<T>(uri: string, data: Record<string, unknown>, option?: RequestConfig): Promise<T> {
    return this.getClient().post(uri, data, option);
  }

  private getClient(): HttpClientInstance {
    if (this.client === null) {
      this.client = this.createClient();
    }
    return this.client;
  }

  private createClient(): HttpClientInstance {
    const client = axios.create(this.config);
    // this.restoreContextHash();

    // client.interceptors.request.use((config) => {
    //   const newConfig = { ...config };
    //   newConfig.headers["x-sharex-gateway-request-context-hash"] =
    //     this.contextHash;
    //   return newConfig;
    // });

    client.interceptors.response.use(
      (response) => {
        // if (
        //   response.headers?.["content-type"] === "text/html;charset=utf-8" &&
        //   !response.headers["x-shared-client-operatable-response"]
        // ) {
        //   this.handleExpired();
        //   throw new axios.Cancel("Expired");
        // }
        // const contextHashNew =
        //   response.headers?.["x-sharex-gateway-reponse-context-hash-new"];
        // if (contextHashNew) {
        //   this.updateContextHash(contextHashNew);
        // }

        const { data } = response;

        if (data && Array.isArray(data.errors) && data.errors.length > 0) {
          return Promise.reject(this.buildApiError(data));
        }

        return data.data || data;
      },
      (error) => this.processApiError(error)
    );

    return client;
  }
}

export default HttpClient;
