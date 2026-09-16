export type LoginCredentials = {
  username: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  user: {
    userId: string;
    username: string;
    email: string;
  };
};
