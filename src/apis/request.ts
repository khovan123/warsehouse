import { GRAPHQL } from './constants';
import HttpClient from './httpClient';

let instance: HttpClient | null = null;
export const getClient = () => {
  // const userId = localStorage.getItem("loginId");
  if (instance === null) {
    instance = new HttpClient({
      baseURL: import.meta.env.VITE_API_URL,
      timeout: Number.parseInt(import.meta.env.VITE_API_TIMEOUT, 10),
      responseType: 'json',
      withCredentials: true,
      // headers: {
      //   "x-sharex-authtoken-userid": userId,
      // },
    });
  }
  return instance;
};

export const callGraphQL = async <T>(
  query: string,
  variables?: Record<string, unknown>
): Promise<T> => {
  return await getClient().post<T>(GRAPHQL, { query, variables });
};
