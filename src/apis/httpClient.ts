import axios, { AxiosError, type AxiosResponse } from 'axios';

import type { HttpClientInstance, RequestConfig } from '../@types/request';

import { ApiError, type OtherErrorInfo } from './type';

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

  private buildApiError(data: unknown, status: number | undefined): ApiError {
    let errorCode;
    let errorMessage;
    let errorDetail;
    let otherErrorInfo;

    const refinedData = data as Record<string, unknown>;

    if (refinedData.errorCodes) {
      const { errorCodes, errorMessages, errorDetails } = refinedData;

      errorCode = Array.isArray(errorCodes) ? (errorCodes as Array<string>)[0] : errorCodes;

      errorMessage = Array.isArray(errorMessages)
        ? (errorMessages as Array<string>)[0]
        : errorMessages;

      errorDetail = Array.isArray(errorDetails) ? (errorDetails as Array<string>)[0] : errorDetails;
    } else if (refinedData.errorCode) {
      errorCode = refinedData.errorCode;
      errorMessage = refinedData.errorMessage;
      errorDetail = refinedData.errorDetail;
    }

    if (refinedData.errorList) {
      otherErrorInfo = {
        validationError: {
          pageErrorFocus: refinedData.pageErrorFocus,
          errorType: refinedData.errorType,
          errorList: refinedData.errorList,
          isConfirmMode: refinedData.isConfirmMode,
        },
      };
    }

    return new ApiError(
      errorCode as string,
      errorMessage as string,
      errorDetail as string,
      otherErrorInfo as OtherErrorInfo,
      status
    );
  }

  private processApiError = (error: {
    response?: { status: number; data: Record<string, unknown> };
  }): Promise<unknown> => {
    let apiError = null;

    if ((error as AxiosError).response) {
      const { data, status } = error.response as AxiosResponse;
      if (data) {
        apiError = this.buildApiError(data, status);
      } else {
        apiError = new ApiError(
          'UNKNOWN_ERROR',
          'UNKNOWN_ERROR',
          'UNKNOWN_ERROR',
          undefined,
          status
        );
      }
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

        return Array.isArray(response.data) ? response.data[0] : response.data;
      },
      (error) => this.processApiError(error)
    );

    return client;
  }
}

export default HttpClient;
