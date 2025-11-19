import { BarChart3, Link2, Truck } from 'lucide-react';

import { Button } from '@/components/ui/button';

const LandingPage: React.FC = () => {
  return (
    <>
      <header className="pt-10 pb-12 lg:pt-16 lg:pb-16 flex flex-col lg:flex-row items-center gap-10">
        <div className="flex-1 space-y-5">
          <span className="inline-flex items-center rounded-full bg-primary px-3 py-1 text-[11px] font-semibold text-primary-foreground uppercase tracking-wide">
            Unified Commerce Simplified
          </span>

          <h1 className="text-3xl sm:text-4xl lg:text-5xl font-semibold tracking-tight text-foreground">
            Enhance shopping experience,
            <span className="block text-primary mt-1">optimize retail operations.</span>
          </h1>

          <p className="text-sm sm:text-base text-muted-foreground max-w-xl">
            Openbravo provides a flexible platform for retail chains: from POS, OMS, inventory to to
            analytics — all in one open and easy-to-integrate system.
          </p>

          <div className="flex flex-wrap items-center gap-3">
            <Button variant={'default'} className="rounded-full px-5 py-2.5 shadow-md">
              Start your free trial
            </Button>
            <Button
              variant={'ghost'}
              className="border border-border rounded-full px-5 py-2.5 shadow-md "
            >
              Learn system architecture
            </Button>
          </div>

          <div className="flex flex-wrap items-center gap-4 text-[11px] sm:text-xs text-muted-foreground pt-1">
            <span className="inline-flex items-center gap-1.5">
              <Truck className="h-3.5 w-3.5" />
              Fast multi-store deployment
            </span>
            <span className="inline-flex items-center gap-1.5">
              <Link2 className="h-3.5 w-3.5" />
              API-first & easy to integrate
            </span>
            <span className="inline-flex items-center gap-1.5">
              <BarChart3 className="h-3.5 w-3.5" />
              Realtime reporting
            </span>
          </div>
        </div>

        <div className="flex-1 w-full">
          <div className="bg-card rounded-3xl shadow-xl border border-border p-4 sm:p-6">
            <div className="flex justify-between items-start mb-4 gap-3">
              <div>
                <p className="text-[11px] text-muted-foreground uppercase tracking-wide">
                  Today Revenue
                </p>
                <p className="text-xl sm:text-2xl font-semibold text-foreground">125.450.000$</p>
              </div>
              <span className="inline-flex items-center rounded-full bg-primary px-2.5 py-1 text-[11px] font-semibold text-primary-foreground">
                ▲ +12.4% vs yesterday
              </span>
            </div>

            <div className="h-28 rounded-2xl bg-secondary border border-border mb-4 flex items-center justify-center text-[11px] text-muted">
              Revenue chart
            </div>

            <div className="grid grid-cols-3 gap-3 text-xs">
              <div className="rounded-xl bg-secondary border border-border px-3 py-2">
                <p className="text-muted">Order</p>
                <p className="font-semibold text-foreground text-sm">324</p>
                <p className="text-[10px] text-primary mt-1">+8.2%</p>
              </div>
              <div className="rounded-xl bg-secondary border border-border px-3 py-2">
                <p className="text-muted">New customer</p>
                <p className="font-semibold text-foreground text-sm">57</p>
                <p className="text-[10px] text-primary mt-1">+3.5%</p>
              </div>
              <div className="rounded-xl bg-secondary border border-border px-3 py-2">
                <p className="text-muted">Inventory</p>
                <p className="font-semibold text-foreground text-sm">8.214</p>
                <p className="text-[10px] text-accent mt-1">Across 12 stores</p>
              </div>
            </div>
          </div>
        </div>
      </header>

      <section id="features" className="flex-1 max-w-6xl w-full mx-auto py-10 space-y-10">
        <div className="grid gap-6 md:grid-cols-3">
          <div className="bg-card border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 01
            </span>
            <h3 className="font-semibold text-foreground mb-2 text-lg">Sales management</h3>
            <p className="text-sm text-muted-foreground">
              Track orders, inventory, and revenue in real time on an intuitive interface.
            </p>
          </div>

          <div className="bg-card border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 02
            </span>
            <h3 className="font-semibold text-foreground mb-2 text-lg">Optimize operations</h3>
            <p className="text-sm text-muted-foreground">
              Synchronize data between retail channels, reducing manual operations and errors in the
              process.
            </p>
          </div>

          <div className="bg-card border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 03
            </span>
            <h3 className="font-semibold text-foreground mb-2 text-lg">Smart reporting</h3>
            <p className="text-sm text-muted-foreground">
              Realtime dashboards and customizable reports help make decisions faster and more
              accurately.
            </p>
          </div>
        </div>

        <div
          id="pricing"
          className="bg-linear-to-r from-primary to-accent rounded-3xl px-6 sm:px-10 py-9 flex flex-col md:flex-row items-start md:items-center justify-between gap-6 shadow-lg"
        >
          <div>
            <h2 className="text-secondary-foreground text-2xl sm:text-3xl font-semibold mb-2">
              Ready to expand your retail system?
            </h2>
            <p className="text-sm text-secondary-foreground/80 max-w-xl">
              Experience Openbravo with full POS, OMS, inventory, customer and reporting features in
              just minutes of setup.
            </p>
          </div>
          <div className="flex gap-3">
            <Button variant={'secondary'} className="rounded-full px-5 py-2.5">
              Sign up for a trial
            </Button>
            <Button className="rounded-full px-5 py-2.5 border border-secondary-foreground/40">
              See live demo
            </Button>
          </div>
        </div>
      </section>
    </>
  );
};

export default LandingPage;
