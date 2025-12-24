/* eslint-disable no-console */
import axios, { AxiosError } from 'axios';
import Cookies from 'js-cookie';

import type { RefreshTokenResponse } from '@/@types/refresh-token';
import type { HttpClientInstance, HttpResponse, RequestConfig } from '@/@types/request';
import { logout, setToken } from '@/state/ducks/auth/slice';
import store from '@/state/store';

import { REFRESH_TOKEN_PATH, UNAUTHORIZED_STATUS_CODE } from './constants';
import { ApiError } from './type';

class HttpClient {
  private config: RequestConfig;
  private client: HttpClientInstance = axios.create();
  // private contextHash: string | null = null;
  private refreshPromise: Promise<string> | null = null;

  constructor(config: RequestConfig) {
    this.config = config;
    this.client = this.createClient();
  }
  // private updateContextHash(newHash: string): void {
  //   this.contextHash = newHash;
  //   document.cookie = `x-sharex-context-hash=${newHash}; path=/; secure;`;
  // }

  private getCookieValue(key: string) {
    return Cookies.get(key);
  }

  // private restoreContextHash(): void {
  //   this.contextHash = this.getCookieValue('x-sharex-context-hash');
  // }

  // private handleExpired(): void {
  //   console.warn('Session expired. Redirect to login...');
  // }

  private buildApiError(error: unknown): ApiError {
    // let errorCode;
    // let errorMessage;
    // let errorDetail;
    // let errorPath;

    // if ((error as GraphqlError).errors) {
    //   const refinedData = error as Record<string, unknown>;
    //   if (Array.isArray(refinedData.errors) && refinedData.errors.length > 0) {
    //     const gqlError = refinedData.errors[0];
    //     const extensions = gqlError.extensions || {};

    //     errorMessage = gqlError.message;

    //     errorCode = extensions.code;

    //     errorDetail =
    //       extensions.stacktrace ||
    //       (Array.isArray(extensions.stacktrace)
    //         ? extensions.stacktrace.join(',')
    //         : extensions.stacktrace) ||
    //       gqlError.message;

    //     errorPath = gqlError.path;

    //     return new ApiError(
    //       errorCode as string,
    //       errorMessage as string,
    //       errorDetail as string,
    //       errorPath as string,
    //       errorDetail as string,
    //       extensions
    //     );
    //   }
    // }
    if (error as AxiosError) {
      const { status, code, message, config, stack, response } = error as AxiosError;

      const { data } = response as HttpResponse;

      if (data as ApiError) {
        return data as ApiError;
      }

      return new ApiError(
        status ?? 500,
        code ?? 'UNKNOWN_ERROR_TILTLE',
        message ?? 'UNKNOWN_ERROR_DETAIL',
        [config?.baseURL, config?.url].join('') ?? 'UNKNOW_ERROR_PATH',
        stack ?? 'UNKNOWN_ERROR_TRACEID',
        {}
      );
    }

    return new ApiError(
      500,
      'UNKNOWN_ERROR_TILTLE',
      'UNKNOWN_ERROR_DETAIL',
      'UNKNOW_ERROR_PATH',
      'UNKNOWN_ERROR_TRACEID',
      {}
    );
  }

  private processApiError = (error: unknown): Promise<unknown> => {
    let apiError = null;
    if (
      // (error as GraphqlError).errors ||
      error as AxiosError
    ) {
      apiError = this.buildApiError(error);
    } else {
      apiError = new ApiError(
        500,
        'UNKNOWN_ERROR_TILTLE',
        'UNKNOWN_ERROR_DETAIL',
        'UNKNOW_ERROR_PATH',
        'UNKNOWN_ERROR_TRACEID',
        {}
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

  private async refreshAccessToken(client: ReturnType<typeof axios.create>): Promise<string> {
    if (!this.refreshPromise) {
      console.log(
        `Call refresh token at path: ${REFRESH_TOKEN_PATH}, with refresh-token: ${this.getCookieValue('refresh-token')}`
      );
      this.refreshPromise = (async () => {
        const res: RefreshTokenResponse = await client.post(REFRESH_TOKEN_PATH, null, {
          withCredentials: true,
        });

        const accessToken = res.data?.accessToken;

        if (!accessToken) {
          throw new Error('Missing accessToken from refresh response');
        }

        store.dispatch({ type: setToken.type, payload: accessToken });

        return accessToken;
      })().finally(() => {
        this.refreshPromise = null;
      });
    }

    return this.refreshPromise;
  }

  private createClient(): HttpClientInstance {
    const client = axios.create({ ...this.config, withCredentials: true });
    // this.restoreContextHash();
    console.log(`Call api with refresh-token: ${this.getCookieValue('refresh-token')}`);
    client.interceptors.request.use((config) => {
      const newConfig = { ...config };
      const token = store.getState().auth.data?.token;
      if (token) {
        newConfig.headers = newConfig.headers ?? {};
        newConfig.headers.Authorization = `Bearer ${token}`;
      }
      // newConfig.headers['x-sharex-gateway-request-context-hash'] = this.contextHash;
      return newConfig;
    });

    client.interceptors.response.use(
      async (response) => {
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
      async (error) => {
        const err = error as AxiosError;
        const status = err.response?.status;
        const originalConfig = err.config as RequestConfig | undefined;

        if (!originalConfig || status !== UNAUTHORIZED_STATUS_CODE) {
          return this.processApiError(error);
        }

        if (originalConfig.url?.includes(REFRESH_TOKEN_PATH)) {
          return this.processApiError(error);
        }

        if (originalConfig.__isRetry) {
          return this.processApiError(error);
        }

        originalConfig.__isRetry = true;

        try {
          const newToken = await this.refreshAccessToken(client);
          originalConfig.headers = originalConfig.headers ?? {};
          originalConfig.headers.Authorization = `Bearer ${newToken}`;
          return client.request(originalConfig);
        } catch (_err) {
          store.dispatch({ type: logout.type });
          return this.processApiError(error);
        }
      }
    );

    return client;
  }
}

export default HttpClient;
