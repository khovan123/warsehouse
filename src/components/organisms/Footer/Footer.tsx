import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer className="border-t border-border bg-surface">
      <div className="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-6 flex flex-col sm:flex-row items-center justify-between gap-4 text-xs sm:text-sm text-muted">
        <p className="text-center sm:text-left">
          © {new Date().getFullYear()} Openbravo. All rights reserved.
        </p>

        <div
          id="support"
          className="flex flex-wrap items-center gap-4 justify-center sm:justify-end"
        >
          <a href="#privacy" className="hover:text-secondary-foreground transition">
            Privacy policy
          </a>
          <a href="#terms" className="hover:text-secondary-foreground transition">
            Terms of use
          </a>
          <a href="#contact" className="hover:text-secondary-foreground transition">
            Contact support
          </a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
