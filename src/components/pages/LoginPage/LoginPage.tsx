import React, { useCallback, useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router';
import { toast } from 'react-toastify';

import { TOP_PATH } from '../../../routers/route.constants';
import { useAuthSelector } from '../../../state/ducks/auth/selectors';
import { loginRequest } from '../../../state/ducks/auth/slice';
import type { LoginData } from '../../../state/ducks/auth/type';
import TextInput from '../../atoms/TextInput';

const LoginPage: React.FC = () => {
  const dispatch = useDispatch();
  const authSelector = useAuthSelector();
  const navigate = useNavigate();
  const [credentials, setCredentials] = useState<LoginData>({
    username: '',
    password: '',
  });

  const handleValueChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    setCredentials((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  }, []);

  const handleLogin = useCallback(
    (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      dispatch(loginRequest(credentials));
    },
    [credentials, dispatch]
  );

  useEffect(() => {
    if (authSelector.logined) {
      navigate(TOP_PATH);
      toast.success('Login successfully!');
    } else if (authSelector.error) {
      toast.error(authSelector.error);
    }
  }, [authSelector, navigate]);

  return (
    <main className="flex items-center justify-center min-h-screen bg-gray-50">
      <form onSubmit={handleLogin} className="w-full max-w-sm p-8 bg-white rounded-lg shadow-md">
        <h1 className="text-2xl font-semibold text-center mb-6 text-gray-800">Login</h1>

        <TextInput
          label="Username"
          name="username"
          value={credentials.username}
          placeholder="Enter your username"
          onChange={handleValueChange}
        />

        <TextInput
          label="Password"
          name="password"
          type="password"
          value={credentials.password}
          placeholder="Enter your password"
          onChange={handleValueChange}
        />

        <div className="flex items-center justify-between mb-4">
          <label className="flex items-center text-sm text-gray-600">
            <input type="checkbox" className="mr-2 rounded" />
            Remember me
          </label>
          <span className="text-sm text-blue-600 hover:underline cursor-pointer">
            Forgot password?
          </span>
        </div>

        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
        >
          Login
        </button>

        <div className="mt-4 text-center text-sm text-gray-600">
          <span>Don’t have an account? </span>
          <button type="button" className="text-blue-600 hover:underline">
            Register
          </button>
        </div>
      </form>
    </main>
  );
};

export default LoginPage;
