import Header from '../../organisms/Header/Header';

const LandingPage: React.FC = () => {
  return (
    <>
      <Header />

      <section
        id="features"
        className="flex-1 max-w-6xl w-full mx-auto px-4 sm:px-6 lg:px-8 py-10 space-y-10"
      >
        <div className="grid gap-6 md:grid-cols-3">
          <div className="bg-surface border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 01
            </span>
            <h3 className="font-semibold text-secondary-foreground mb-2 text-lg">
              Sales management
            </h3>
            <p className="text-sm text-muted">
              Track orders, inventory, and revenue in real time on an intuitive interface.
            </p>
          </div>

          <div className="bg-surface border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 02
            </span>
            <h3 className="font-semibold text-secondary-foreground mb-2 text-lg">
              Optimize operations
            </h3>
            <p className="text-sm text-muted">
              Synchronize data between retail channels, reducing manual operations and errors in the
              process.
            </p>
          </div>

          <div className="bg-surface border border-border rounded-2xl p-5 shadow-sm">
            <span className="inline-block text-[11px] font-semibold uppercase tracking-wide text-primary mb-2">
              Feature 03
            </span>
            <h3 className="font-semibold text-secondary-foreground mb-2 text-lg">
              Smart reporting
            </h3>
            <p className="text-sm text-muted">
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
            <button className="inline-flex items-center justify-center rounded-full px-5 py-2.5 text-sm font-medium bg-secondary text-secondary-foreground hover:bg-dark transition">
              Sign up for a trial
            </button>
            <button className="inline-flex items-center justify-center rounded-full px-5 py-2.5 text-sm font-medium border border-secondary-foreground/40 text-secondary-foreground hover:bg-primary-light/30 transition">
              See live demo
            </button>
          </div>
        </div>
      </section>
    </>
  );
};

export default LandingPage;
