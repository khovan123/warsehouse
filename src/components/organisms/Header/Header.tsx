import React from 'react';

const Header: React.FC = () => {
  return (
    <header className="bg-background">
      <div className="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 pt-10 pb-12 lg:pt-16 lg:pb-16 flex flex-col lg:flex-row items-center gap-10">
        <div className="flex-1 space-y-5">
          <span className="inline-flex items-center rounded-full bg-primary-light px-3 py-1 text-[11px] font-semibold text-primary-foreground uppercase tracking-wide">
            Unified Commerce Simplified
          </span>

          <h1 className="text-3xl sm:text-4xl lg:text-5xl font-semibold tracking-tight text-secondary-foreground">
            Enhance shopping experience,
            <span className="block text-primary mt-1">optimize retail operations.</span>
          </h1>

          <p className="text-sm sm:text-base text-muted max-w-xl">
            Openbravo provides a flexible platform for retail chains: from POS, OMS, inventory to to
            analytics — all in one open and easy-to-integrate system.
          </p>

          <div className="flex flex-wrap items-center gap-3">
            <button className="inline-flex items-center justify-center rounded-full px-5 py-2.5 text-sm font-medium bg-primary text-primary-foreground hover:bg-primary-dark transition shadow-md">
              Start your free trial
            </button>
            <button className="inline-flex items-center justify-center rounded-full px-5 py-2.5 text-sm font-medium border border-border text-secondary-foreground hover:bg-surface transition">
              Learn system architecture
            </button>
          </div>

          <div className="flex flex-wrap items-center gap-4 text-[11px] sm:text-xs text-muted pt-1">
            <span>⚡ Fast multi-store deployment</span>
            <span>•</span>
            <span>🔌 API-first & easy to integrate</span>
            <span>•</span>
            <span>📊 Realtime reporting</span>
          </div>
        </div>

        <div className="flex-1 w-full">
          <div className="bg-surface rounded-3xl shadow-xl border border-border p-4 sm:p-6">
            <div className="flex justify-between items-start mb-4 gap-3">
              <div>
                <p className="text-[11px] text-muted uppercase tracking-wide">Today Revenue</p>
                <p className="text-xl sm:text-2xl font-semibold text-secondary-foreground">
                  125.450.000$
                </p>
              </div>
              <span className="inline-flex items-center rounded-full bg-primary-light px-2.5 py-1 text-[11px] font-semibold text-primary-foreground">
                ▲ +12.4% vs yesterday
              </span>
            </div>

            <div className="h-28 rounded-2xl bg-dark/60 border border-border mb-4 flex items-center justify-center text-[11px] text-muted">
              Revenue chart
            </div>

            <div className="grid grid-cols-3 gap-3 text-xs">
              <div className="rounded-xl bg-dark/60 border border-border px-3 py-2">
                <p className="text-muted">Order</p>
                <p className="font-semibold text-secondary-foreground text-sm">324</p>
                <p className="text-[10px] text-primary mt-1">+8.2%</p>
              </div>
              <div className="rounded-xl bg-dark/60 border border-border px-3 py-2">
                <p className="text-muted">New customer</p>
                <p className="font-semibold text-secondary-foreground text-sm">57</p>
                <p className="text-[10px] text-primary mt-1">+3.5%</p>
              </div>
              <div className="rounded-xl bg-dark/60 border border-border px-3 py-2">
                <p className="text-muted">Inventory</p>
                <p className="font-semibold text-secondary-foreground text-sm">8.214</p>
                <p className="text-[10px] text-accent mt-1">Across 12 stores</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
