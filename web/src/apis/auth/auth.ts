import type { LoginCredentials, LoginResponse } from '@/@types/auth';

import { LOGIN_PATH } from '../constants';
import { callGraphQL, getClient } from '../request';

const LOGIN_MUTATION = `
  mutation Login($username: String!, $password: String!) {
    login(username: $username, password: $password) {
      token
      user {
        username
        email
        userId
      }
    }
  }
`;

export const loginGraphApi = async (credentials: LoginCredentials) => {
  type Response = { login: LoginResponse };

  const data = await callGraphQL<Response>(LOGIN_MUTATION, {
    username: credentials.username,
    password: credentials.password,
  });

  return data.login;
};

export const loginApi = async (credentials: LoginCredentials) =>
  await getClient().post<LoginCredentials>(LOGIN_PATH, credentials);
