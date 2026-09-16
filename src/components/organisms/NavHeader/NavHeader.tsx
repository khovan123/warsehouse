import React from 'react';
import { Link } from 'react-router';

import { cn } from '@/lib/utils';
import { LOGIN_PATH } from '@/routers/route.constants';

type MenuItem = { path: string; name: string; customStyle?: string };

const PUBLIC_MENU: MenuItem[] = [
  {
    path: '#features',
    name: 'Features',
  },
  {
    path: '#pricing',
    name: 'Prices',
  },
  {
    path: '#support',
    name: 'Support',
  },
];

const Nav: React.FC = () => {
  return (
    <nav className={cn('sticky top-0 z-30 bg-card/95 backdrop-blur border-b border-border')}>
      <div className={cn('max-w-7xl mx-auto h-14 sm:h-16 flex items-center justify-between')}>
        <Link to="/" className="flex items-center gap-2">
          <div className="h-8 w-8 rounded-md bg-primary flex items-center justify-center">
            <span className="text-xs font-bold text-primary-foreground">OB</span>
          </div>
          <div className="flex flex-col leading-tight">
            <span className="font-semibold text-foreground text-sm">Openbravo</span>
            <span className="text-[10px] uppercase tracking-[0.15em] text-muted-foreground">
              Commerce &amp; ERP Platform
            </span>
          </div>
        </Link>

        <div className="hidden md:flex items-center gap-6 text-xs sm:text-sm font-medium text-muted">
          {PUBLIC_MENU.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              tabIndex={1}
              className="text-foreground hover:text-primary transition"
            >
              {item.name}
            </Link>
          ))}
        </div>

        <div className="flex items-center gap-3">
          <Link
            to={LOGIN_PATH}
            className="text-xs sm:text-sm font-medium hover:text-primary transition"
          >
            Log in
          </Link>
          <Link
            to={LOGIN_PATH}
            className="hidden sm:inline-flex items-center justify-center rounded-full px-4 py-1.5 text-xs sm:text-sm font-medium bg-primary text-primary-foreground hover:bg-primary/80 transition shadow-sm"
          >
            Get started now
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Nav;
