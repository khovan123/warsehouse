import React from 'react';
import { Link } from 'react-router';

import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { PRODUCT_MANAGEMENT_PATH } from '@/routers/route.constants';

export const NotFoundPage: React.FC = () => {
  const isMobile = useIsMobile();

  return (
    <main className="relative min-h-screen flex items-center justify-center bg-linear-to-br from-secondary via-background to-black text-secondary-foreground px-4">
      <div className="pointer-events-none absolute inset-0 overflow-hidden">
        <div className="absolute -top-10 -left-10 h-40 w-40 rounded-full bg-primary/20 blur-3xl animate-pulse" />
        <div className="absolute -bottom-16 -right-10 h-52 w-52 rounded-full bg-accent/20 blur-3xl animate-pulse" />
      </div>

      <div className="absolute h-56 w-56 rounded-full border border-primary/40 blur-xl" />

      <section className="relative z-10 w-full max-w-lg rounded-3xl border border-border/70 bg-card/80 px-8 py-10 shadow-2xl backdrop-blur-xl text-foreground">
        <div className="flex flex-col items-center gap-4 text-center">
          <span className="inline-flex items-center gap-2 rounded-full border border-border bg-background/60 px-4 py-1.5 text-[11px] font-medium uppercase tracking-[0.18em] text-muted-foreground">
            <span className="h-1.5 w-1.5 rounded-full bg-primary animate-pulse" />
            Page not found
          </span>

          <div className="relative">
            <span className="absolute -inset-1 rounded-full bg-primary/10 blur-xl" />
            <h1 className="relative text-6xl font-semibold tracking-tight bg-linear-to-br from-primary via-primary/40 to-accent bg-clip-text text-transparent animate-pulse">
              404
            </h1>
          </div>

          <div className="space-y-2">
            <h2 className="text-xl font-semibold">Oops, we couldn’t find that page.</h2>
            <p className="text-sm text-muted-foreground">
              The link may be broken or the page may have been removed. You can go back to your
              Openbravo workspace and continue from there.
            </p>
          </div>

          <div
            className={cn(
              'mt-4 flex items-center justify-center gap-3 w-full',
              isMobile ? 'flex-col' : 'flex-row'
            )}
          >
            <Link
              to={PRODUCT_MANAGEMENT_PATH}
              className={cn(
                'inline-flex items-center justify-center gap-2 rounded-full bg-primary px-6 py-2.5 text-sm font-semibold text-secondary shadow-lg shadow-primary/40 hover:bg-primary/80 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/40 transition',
                isMobile ? 'w-full' : 'w-auto'
              )}
            >
              <span>Go Home</span>
              <span className="inline-block translate-x-0 transition-transform group-hover:translate-x-0.5">
                ↗
              </span>
            </Link>

            <button
              type="button"
              className={cn(
                'inline-flex items-center justify-center gap-2 rounded-full border border-border px-6 py-2.5 text-sm font-medium text-foreground hover:bg-primary/30 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary/40 transition',
                isMobile ? 'w-full' : 'w-auto'
              )}
            >
              Contact support
            </button>
          </div>
        </div>
      </section>
    </main>
  );
};

export default NotFoundPage;
