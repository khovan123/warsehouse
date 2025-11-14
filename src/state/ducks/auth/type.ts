export type LoginData = {
  username: string;
  password: string;
};

export type User = {
  name: string;
};

export type AuthState = {
  data: User | null;
  loading: boolean;
  error: string | null;
  logined: boolean;
};

export const INIT_AUTH: AuthState = {
  data: null,
  loading: false,
  error: null,
  logined: false,
};
