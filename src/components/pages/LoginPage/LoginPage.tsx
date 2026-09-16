import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Link, useNavigate } from 'react-router';

import { LANDING_PATH, PRODUCT_MANAGEMENT_PATH } from '../../../routers/route.constants';
import { useAuthSelector } from '../../../state/ducks/auth/selectors';
import { loginRequest } from '../../../state/ducks/auth/slice';
import type { LoginData } from '../../../state/ducks/auth/type';
import TextBox from '../../atoms/TextBox/TextBox';
import InputForm from '../../organisms/InputForm/InputForm';
import { Avatar, AvatarFallback, AvatarImage } from '../../ui/avatar';
import { Button } from '../../ui/button';
import { Checkbox } from '../../ui/checkbox';
import { Label } from '../../ui/label';

const LoginPage: React.FC = () => {
  const dispatch = useDispatch();
  const authSelector = useAuthSelector();
  const navigate = useNavigate();

  const handleLogin = (data: LoginData) => {
    dispatch(loginRequest(data));
  };

  useEffect(() => {
    if (authSelector.logined) {
      navigate(PRODUCT_MANAGEMENT_PATH);
    }
  }, [authSelector.logined, navigate]);

  const defaultValues: LoginData = {
    username: '',
    password: '',
  };

  return (
    <main className="min-h-screen flex text-foreground">
      <section className="hidden lg:flex lg:flex-1 flex-col justify-between px-12 py-10 bg-linear-to-br from-secondary via-primary-foreground to-black text-secondary-foreground">
        <header className="flex items-center gap-3">
          <Link to={LANDING_PATH}>
            <Avatar>
              <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
              <AvatarFallback>CN</AvatarFallback>
            </Avatar>
          </Link>
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
          <p className="text-sm text-muted-foreground max-w-md">
            Log in to access the Openbravo platform – where you manage POS, orders, inventory and
            retail data in a modern web experience.
          </p>

          <div className="mt-10 grid grid-cols-2 gap-4 max-w-md text-sm">
            <div className="rounded-2xl border border-border bg-card/80 p-4 backdrop-blur">
              <p className="text-[11px] font-medium text-muted-foreground mb-1 uppercase tracking-[0.18em]">
                Real-time
              </p>
              <p className="font-semibold text-foreground">Inventory visibility</p>
            </div>
            <div className="rounded-2xl border border-border bg-card/80 p-4 backdrop-blur">
              <p className="text-[11px] font-medium text-muted-foreground mb-1 uppercase tracking-[0.18em]">
                Omnichannel
              </p>
              <p className="font-semibold text-foreground">Retail experiences</p>
            </div>
          </div>
        </div>

        <footer className="text-[11px] text-muted-foreground">
          © {new Date().getFullYear()} Openbravo. All rights reserved.
        </footer>
      </section>

      <section className="flex-1 flex items-center justify-center px-4 py-10 lg:px-10">
        <InputForm<LoginData> defaultValues={defaultValues} onSubmit={handleLogin}>
          <>
            <div className="flex items-center justify-between gap-4 mb-2">
              <div>
                <p className="text-xs font-medium text-muted-foreground">Welcome to</p>
                <h2 className="text-2xl font-semibold tracking-tight">Openbravo</h2>
              </div>
              <Avatar className="lg:hidden">
                <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
                <AvatarFallback>CN</AvatarFallback>
              </Avatar>
            </div>

            <p className="text-xs text-muted-foreground">
              Sign in with your Openbravo account to continue.
            </p>

            <div className="space-y-4">
              <TextBox id="username" label="Username" name="username" />

              <TextBox id="password" label="Password" name="password" type="password" />
            </div>

            <div className="flex items-center justify-between text-[11px] text-muted-foreground">
              <Label
                htmlFor="remember"
                className="flex items-center gap-2 cursor-pointer select-none"
              >
                <Checkbox id="remember" />
                <span>Remember me</span>
              </Label>

              <Button variant={'link'} type="button">
                Forgot password?
              </Button>
            </div>

            <Button type="submit" className="w-full rounded-full">
              {authSelector.loading ? 'Loading…' : 'Log in'}
            </Button>

            <div className="relative">
              <div className="absolute inset-0 flex items-center">
                <span className="w-full border-t border-border" />
              </div>
              <div className="relative flex justify-center">
                <span className="bg-card px-3 text-[10px] uppercase tracking-[0.18em] text-muted-foreground">
                  or
                </span>
              </div>
            </div>

            <Button
              type="button"
              variant={'outline'}
              className="w-full rounded-full border border-border hover:bg-primary/20"
            >
              Sign in with SSO
            </Button>

            <p className="text-[11px] text-center text-muted-foreground">
              Don’t have an account?
              <Button
                variant={'link'}
                type="button"
                className="font-medium text-primary hover:text-primary/80 transition-colors"
              >
                Contact your Openbravo admin
              </Button>
            </p>
          </>
        </InputForm>
      </section>
    </main>
  );
};

export default LoginPage;
