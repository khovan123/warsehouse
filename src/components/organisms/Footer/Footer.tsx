import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer className="border-t border-border bg-card">
      <div className="max-w-7xl mx-auto py-4 flex flex-col sm:flex-row items-center justify-between gap-4 text-xs sm:text-sm text-muted-foreground">
        <p className="text-center sm:text-left">
          © {new Date().getFullYear()} Openbravo. All rights reserved.
        </p>

        <div
          id="support"
          className="flex flex-wrap items-center gap-4 justify-center sm:justify-end"
        >
          <a href="#privacy" className="hover:text-foreground transition">
            Privacy policy
          </a>
          <a href="#terms" className="hover:text-foreground transition">
            Terms of use
          </a>
          <a href="#contact" className="hover:text-foreground transition">
            Contact support
          </a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
