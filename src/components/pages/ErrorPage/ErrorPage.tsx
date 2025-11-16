import React from 'react';
import { Link, useNavigate } from 'react-router';

import { TOP_PATH } from '../../../routers/route.constants';

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
    <main className="relative min-h-screen flex items-center justify-center bg-linear-to-br from-background via-secondary to-black text-secondary-foreground px-4">
      <div className="pointer-events-none absolute inset-0 overflow-hidden">
        <div className="absolute -top-10 -right-10 h-32 w-32 rounded-full bg-red-500/20 blur-3xl animate-pulse" />
        <div className="absolute -bottom-16 -left-10 h-48 w-48 rounded-full bg-red-500/10 blur-3xl animate-pulse" />
      </div>

      <div className="absolute h-56 w-56 rounded-full border border-red-500/40 blur-xl" />

      <section className="relative z-10 w-full max-w-lg rounded-3xl border border-border/70 bg-surface/80 px-8 py-10 shadow-2xl backdrop-blur-xl">
        <div className="flex flex-col items-center gap-4 text-center">
          <span className="inline-flex items-center gap-2 rounded-full border border-red-500/40 bg-background/60 px-4 py-1.5 text-[11px] font-medium uppercase tracking-[0.18em] text-red-400">
            <span className="h-1.5 w-1.5 rounded-full bg-red-500 animate-pulse" />
            Unexpected Error
          </span>

          <div className="relative">
            <span className="absolute -inset-1 rounded-full bg-red-500/20 blur-xl" />
            <h1 className="relative text-6xl font-semibold tracking-tight bg-linear-to-br from-red-500 via-red-400 to-red-600 bg-clip-text text-transparent animate-pulse">
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
              className="cursor-pointer inline-flex w-full sm:w-auto items-center justify-center gap-2 rounded-full bg-red-600 px-6 py-2.5 text-sm font-semibold text-white shadow-lg shadow-red-500/40 hover:bg-red-600 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-red-400 transition"
            >
              Try Again
            </button>

            <Link
              to={TOP_PATH}
              className="inline-flex w-full sm:w-auto items-center justify-center gap-2 rounded-full border border-border px-6 py-2.5 text-sm font-medium text-secondary-foreground hover:bg-secondary/80 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-red-300 transition"
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
