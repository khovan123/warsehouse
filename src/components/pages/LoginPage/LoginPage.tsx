import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router';

import { TOP_PATH } from '../../../routers/route.constants';
import { useAuthSelector } from '../../../state/ducks/auth/selectors';
import { loginRequest } from '../../../state/ducks/auth/slice';
import type { LoginData } from '../../../state/ducks/auth/type';
import TextBox from '../../atoms/TextBox/TextBox';
import InputForm from '../../organisms/InputForm/InputForm';

const LoginPage: React.FC = () => {
  const dispatch = useDispatch();
  const authSelector = useAuthSelector();
  const navigate = useNavigate();

  const handleLogin = (data: LoginData) => {
    dispatch(loginRequest(data));
  };

  useEffect(() => {
    if (authSelector.logined) {
      navigate(TOP_PATH);
    }
  }, [authSelector.logined, navigate]);

  const defaultValues: LoginData = {
    username: '',
    password: '',
  };

  return (
    <main className="min-h-screen flex text-secondary-foreground">
      <section className="hidden lg:flex lg:flex-1 flex-col justify-between px-12 py-10 bg-linear-to-br from-secondary via-dark to-black">
        <header className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-full bg-primary text-secondary font-semibold">
            OB
          </div>
          <div className="leading-tight">
            <p className="text-lg font-semibold tracking-wide">Openbravo</p>
            <p className="text-[11px] uppercase text-muted">Commerce &amp; ERP Platform</p>
          </div>
        </header>

        <div className="space-y-6">
          <h1 className="text-4xl font-semibold tracking-tight">
            Unified commerce
            <span className="text-primary">. Simplified.</span>
          </h1>
          <p className="text-sm text-muted max-w-md">
            Log in to access the Openbravo platform – where you manage POS, orders, inventory and
            retail data in a modern web experience.
          </p>

          <div className="mt-10 grid grid-cols-2 gap-4 max-w-md text-sm">
            <div className="rounded-2xl border border-border bg-surface/80 p-4 backdrop-blur">
              <p className="text-[11px] font-medium text-muted mb-1 uppercase tracking-[0.18em]">
                Real-time
              </p>
              <p className="font-semibold">Inventory visibility</p>
            </div>
            <div className="rounded-2xl border border-border bg-surface/80 p-4 backdrop-blur">
              <p className="text-[11px] font-medium text-muted mb-1 uppercase tracking-[0.18em]">
                Omnichannel
              </p>
              <p className="font-semibold">Retail experiences</p>
            </div>
          </div>
        </div>

        <footer className="text-[11px] text-muted">
          © {new Date().getFullYear()} Openbravo. All rights reserved.
        </footer>
      </section>

      <section className="flex-1 flex items-center justify-center px-4 py-10 lg:px-10">
        <InputForm<LoginData> defaultValues={defaultValues} onSubmit={handleLogin}>
          <>
            <div className="flex items-center justify-between gap-4 mb-2">
              <div>
                <p className="text-xs font-medium text-muted">Welcome to</p>
                <h2 className="text-2xl font-semibold tracking-tight">Openbravo</h2>
              </div>
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-primary text-secondary font-semibold lg:hidden">
                OB
              </div>
            </div>

            <p className="text-xs text-muted">Sign in with your Openbravo account to continue.</p>

            <div className="space-y-4">
              <TextBox id="username" label="Username" name="username" />

              <TextBox id="password" label="Password" name="password" type="password" />
            </div>

            <div className="flex items-center justify-between text-[11px] text-muted">
              <label className="flex items-center gap-2 cursor-pointer select-none">
                <input
                  type="checkbox"
                  className="h-3.5 w-3.5 rounded border-border bg-transparent text-primary"
                />
                <span>Remember me</span>
              </label>

              <button
                type="button"
                className="font-medium text-primary hover:text-primary-dark transition-colors"
              >
                Forgot password?
              </button>
            </div>

            <button
              type="submit"
              className="w-full inline-flex items-center justify-center rounded-full bg-primary py-2.5 text-sm font-semibold text-secondary shadow-lg shadow-primary/30 hover:bg-primary-dark focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary-light transition-colors"
            >
              {authSelector.loading ? 'Loading…' : 'Log in'}
            </button>

            <div className="relative">
              <div className="absolute inset-0 flex items-center">
                <span className="w-full border-t border-border" />
              </div>
              <div className="relative flex justify-center">
                <span className="bg-surface px-3 text-[10px] uppercase tracking-[0.18em] text-muted">
                  or
                </span>
              </div>
            </div>

            <button
              type="button"
              className="w-full inline-flex items-center justify-center rounded-full border border-border bg-transparent py-2.5 text-sm font-medium text-secondary-foreground hover:bg-secondary focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary-light transition-colors"
            >
              Sign in with SSO
            </button>

            <p className="text-[11px] text-center text-muted">
              Don’t have an account?
              <button
                type="button"
                className="font-medium text-primary hover:text-primary-dark transition-colors"
              >
                Contact your Openbravo admin
              </button>
            </p>
          </>
        </InputForm>
      </section>
    </main>
  );
};

export default LoginPage;
