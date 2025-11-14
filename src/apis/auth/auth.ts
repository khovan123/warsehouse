import type { LoginCredentials, LoginResponse } from '../../@types/auth';
import { REQUEST_LOGIN_PATH } from '../constants';
import { getClient } from '../request';

export const loginApi = async (credentials: LoginCredentials): Promise<LoginResponse> => {
  return getClient().get<LoginResponse>(REQUEST_LOGIN_PATH, {
    params: { ...credentials },
  });
};
