import type { LoginCredentials, LoginResponse } from '@/@types/auth';
import { REQUEST_LOGIN_PATH } from '@/apis/constants';
import { getClient } from '@/apis/request';

export const loginApi = async (credentials: LoginCredentials): Promise<LoginResponse> => {
  return getClient().get<LoginResponse>(REQUEST_LOGIN_PATH, {
    params: { ...credentials },
  });
};
