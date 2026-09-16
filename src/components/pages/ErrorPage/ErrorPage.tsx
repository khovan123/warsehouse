import React from 'react';
import { Link, useNavigate } from 'react-router';

import { PRODUCT_MANAGEMENT_PATH } from '@/routers/route.constants';

type ErrorPageProps = {
  error?: unknown;
  onRetry?: () => void;
};

export const ErrorPage: React.FC<ErrorPageProps> = ({ error, onRetry }) => {
  const navigate = useNavigate();

  const message =
    error instanceof Error
      ? error.message
      : typeof error === 'string'
        ? error
        : 'An unexpected error occurred in the system.';

  const handleRetry = () => {
    if (onRetry) {
      onRetry();
    } else {
      navigate(0);
    }
  };

  return (
    <main className="relative min-h-screen flex items-center justify-center bg-linear-to-br from-background via-surface to-secondary text-foreground px-4">
      <div className="pointer-events-none absolute inset-0 overflow-hidden">
        <div className="absolute -top-10 -right-10 h-32 w-32 rounded-full bg-destructive/15 blur-3xl opacity-70 animate-pulse" />
        <div className="absolute -bottom-16 -left-10 h-48 w-48 rounded-full bg-destructive/15 blur-3xl opacity-50 animate-pulse" />
      </div>

      <div className="absolute h-56 w-56 rounded-full border border-destructive blur-xl opacity-30" />

      <section className="relative z-10 w-full max-w-lg rounded-3xl border border-border/70 bg-card/80 px-8 py-10 shadow-2xl backdrop-blur-xl">
        <div className="flex flex-col items-center gap-4 text-center">
          <span className="inline-flex items-center gap-2 rounded-full border border-destructive bg-background/80 px-4 py-1.5 text-[11px] font-medium uppercase tracking-[0.18em] text-destructive">
            <span className="h-1.5 w-1.5 rounded-full bg-destructive animate-pulse" />
            Unexpected Error
          </span>

          <div className="relative">
            <span className="absolute -inset-1 rounded-full bg-destructive/15 blur-xl opacity-80" />
            <h1 className="relative text-6xl font-semibold tracking-tight text-destructive drop-shadow-[0_10px_35px_rgba(217,45,32,0.35)]">
              500
            </h1>
          </div>

          <div className="space-y-2">
            <h2 className="text-xl font-semibold">Something went wrong.</h2>
            <p className="text-sm text-muted max-w-md">{message}</p>
          </div>

          <div className="mt-4 flex flex-col sm:flex-row items-center justify-center gap-3 w-full">
            <button
              onClick={handleRetry}
              className="cursor-pointer inline-flex w-full sm:w-auto items-center justify-center gap-2 rounded-full bg-destructive px-6 py-2.5 text-sm font-semibold text-white shadow-[0_20px_45px_rgba(217,45,32,0.35)] hover:bg-destructive-dark focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-destructive transition"
            >
              Try Again
            </button>

            <Link
              to={PRODUCT_MANAGEMENT_PATH}
              className="inline-flex w-full sm:w-auto items-center justify-center gap-2 rounded-full border border-border px-6 py-2.5 text-sm font-medium text-foreground hover:bg-card focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-destructive transition"
            >
              Go Home
            </Link>
          </div>
        </div>
      </section>
    </main>
  );
};

export default ErrorPage;
