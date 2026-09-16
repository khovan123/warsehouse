import React from 'react';
import { Link } from 'react-router';

import { LOGIN_PATH } from '../../../routers/route.constants';

const Nav: React.FC = () => {
  return (
    <nav className="sticky top-0 z-30 bg-surface/95 backdrop-blur border-b border-border">
      <div className="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 h-14 sm:h-16 flex items-center justify-between">
        <Link to="/" className="flex items-center gap-2">
          <div className="h-8 w-8 rounded-md bg-primary flex items-center justify-center">
            <span className="text-xs font-bold text-primary-foreground">OB</span>
          </div>
          <div className="flex flex-col leading-tight">
            <span className="font-semibold text-secondary-foreground text-sm">Openbravo</span>
            <span className="text-[10px] uppercase tracking-[0.15em] text-muted">
              Commerce &amp; ERP Platform
            </span>
          </div>
        </Link>

        <div className="hidden md:flex items-center gap-6 text-xs sm:text-sm font-medium text-muted">
          <a href="#features" className="hover:text-secondary-foreground transition">
            Features
          </a>
          <a href="#pricing" className="hover:text-secondary-foreground transition">
            Prices
          </a>
          <a href="#support" className="hover:text-secondary-foreground transition">
            Support
          </a>
        </div>

        <div className="flex items-center gap-3">
          <Link
            to={LOGIN_PATH}
            className="text-xs sm:text-sm font-medium text-muted hover:text-secondary-foreground transition"
          >
            Log in
          </Link>
          <Link
            to={LOGIN_PATH}
            className="hidden sm:inline-flex items-center justify-center rounded-full px-4 py-1.5 text-xs sm:text-sm font-medium bg-primary text-primary-foreground hover:bg-primary-dark transition shadow-sm"
          >
            Get started now
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Nav;
