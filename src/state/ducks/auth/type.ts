import type { ApiError } from '@/apis/type';

export type LoginData = {
  username: string;
  password: string;
};

export type User = {
  userId: string;
  username: string;
  email: string;
};

export type AuthState = {
  data: {
    user: User;
    token: string;
  } | null;
  loading: boolean;
  error: ApiError | null;
  logined: boolean;
};

export const INIT_AUTH: AuthState = {
  data: null,
  loading: false,
  error: null,
  logined: false,
};
